using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dymbnails.DataAccess {
    static class SqlExtensions {
        private static Materializer materializer;
        static SqlExtensions() {
            materializer = new Materializer();
        }

        public static SqlCommand Call(this SqlConnection connection, String procedureName) {
            var command = connection.CreateCommand();
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public static SqlCommand Query(this SqlConnection connection, String queryText) {
            var command = connection.CreateCommand();
            command.CommandText = queryText;
            command.CommandType = CommandType.Text;
            return command;
        }

        public static SqlCommand InTransaction(this SqlCommand command, SqlTransaction transaction) {
            command.Transaction = transaction;
            return command;
        }

        public static SqlCommand WithParameters(this SqlCommand command, Dictionary<String, Object> parameters) {
            foreach (var parameter in parameters) {
                command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value ?? DBNull.Value));
            }
            return command;
        }

        public static List<TResult> Materialize<TResult>(this SqlCommand command) where TResult : class, new() {
            using (var reader = command.ExecuteReader()) {
                return materializer.Materialize<TResult>(reader);
            }
        }

        public static TResult MaterializeSingle<TResult>(this SqlCommand command) where TResult : class, new() {
            using (var reader = command.ExecuteReader()) {
                return materializer.MaterializeSingle<TResult>(reader);
            }
        }
    }
}
