using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Data.Common;

namespace Dymbnails.DataAccess {
    class Materializer {
        private static Dictionary<int, Delegate> cache;

        public Materializer() {
            cache = new Dictionary<int, Delegate>();
        }

        public List<TResult> Materialize<TResult>(DbDataReader reader) where TResult : class, new() {
            var result = new List<TResult>();
            while (reader.Read()) {
                result.Add(MaterializeSingleInternal<TResult>(reader));
            }
            return result;
        }

        public TResult MaterializeSingle<TResult>(DbDataReader reader) where TResult : class, new() {
            if (reader.Read()) {
                return MaterializeSingleInternal<TResult>(reader);
            }
            return null;
        }

        private TResult MaterializeSingleInternal<TResult>(DbDataReader reader) where TResult : class, new() {
            var materializeId = GetRequestedMaterializeIdentifier(reader, typeof(TResult));
            if (!cache.ContainsKey(materializeId)) {
                cache[materializeId] = GetMaterializer<TResult>(reader);
            }
            return ((Func<DbDataReader, TResult>)cache[materializeId])(reader);
        }

        private int GetRequestedMaterializeIdentifier(DbDataReader reader, Type type) {
            return GetHash(reader) + type.FullName.GetHashCode();
        }

        private int GetHash(DbDataReader reader) {
            var result = 0;
            for (var column = 0; column < reader.FieldCount; column++) {
                result += reader.GetName(column).GetHashCode();
            }
            return result;
        }

        #region New Materialize

        private Func<DbDataReader, TResult> GetMaterializer<TResult>(DbDataReader reader) where TResult : class, new() {
            var root = new RootWithBindings(Expression.New(typeof(TResult)), String.Empty);
            var readerParameter = Expression.Parameter(typeof(DbDataReader), "reader");
            var readerIndexer = typeof(DbDataReader).GetMethod("get_Item", new[] { typeof(Int32) });

            PropertyInfo propertyInfo;
            for (var column = 0; column < reader.FieldCount; column++) {
                    var members = reader.GetName(column).Split('.');

                    var currentRoot = root;

                    for (var index = 0; index < members.Length - 1; index++) {
                        var member = members[index];
                        propertyInfo = currentRoot.Root.Type.GetProperty(member);

                        var newRoot = currentRoot.ObjectBindings.FirstOrDefault(b => b.Value.Name == member).Value;
                        if (newRoot == null) {
                            if (propertyInfo.PropertyType.GetConstructor(Type.EmptyTypes) == null) {
                                throw new MissingMemberException("No public parameterless constructor found on '" +
                                    propertyInfo.PropertyType.FullName + "' type.");
                            }

                            // Creating new descendent object
                            newRoot = new RootWithBindings(Expression.New(propertyInfo.PropertyType), member);
                            currentRoot.ObjectBindings.Add(propertyInfo, newRoot);
                        }
                        currentRoot = newRoot;
                    }

                    // Assigning direct property on last descendent object
                    propertyInfo = currentRoot.Root.Type.GetProperty(members[members.Length - 1]);
                    if (reader[column].GetType() != propertyInfo.PropertyType &&
                        reader[column].GetType() != typeof(DBNull)) {
                        throw new InvalidCastException("Property '" + typeof(TResult).Name + "." + reader.GetName(column) + "' is of type '" +
                            propertyInfo.PropertyType + "' when database returned '" + reader[column].GetType() + "' value.");
                    }
                    var valueExpression = Expression.Call(readerParameter, readerIndexer, Expression.Constant(column));
                
                    currentRoot.MemberBindings.Add(Expression.Bind(propertyInfo,
                        Expression.Convert(
                            Expression.Condition(Expression.Equal(valueExpression, Expression.Constant(DBNull.Value)),
                                                    Expression.Constant(null), valueExpression),
                            propertyInfo.PropertyType)
                        )
                    );
                }

            return Expression.Lambda<Func<DbDataReader, TResult>>(BuildTree(root), readerParameter).Compile();
        }

        private Expression BuildTree(RootWithBindings root) {
            var bindings = new List<MemberBinding>(root.MemberBindings);
            foreach (var binding in root.ObjectBindings) {
                bindings.Add(Expression.Bind(binding.Key, BuildTree(binding.Value)));
            }
            return Expression.MemberInit(root.Root, bindings);
        }

        private class RootWithBindings {
            public String Name { get; set; }

            public NewExpression Root { get; set; }
            public List<MemberBinding> MemberBindings { get; private set; }

            public Dictionary<PropertyInfo, RootWithBindings> ObjectBindings { get; private set; }

            public RootWithBindings(NewExpression root, String name) {
                Name = name;
                Root = root;
                MemberBindings = new List<MemberBinding>();
                ObjectBindings = new Dictionary<PropertyInfo, RootWithBindings>();
            }
        }

        #endregion
    }
}
