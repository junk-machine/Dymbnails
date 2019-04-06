using System.Collections.Generic;
using Dymbnails.Entities;
using Dymbnails.DataAccess.Interfaces;

namespace Dymbnails.DataAccess.Components {
    class VariablesComponent : ComponentBase, IVariablesComponent {
        public VariablesComponent(ConnectionManager manager) : base(manager) { }

        public List<Variable> GetList(int dymbnailId) {
            return Procedure("Variables_GetList")
                .WithParameters(new Dictionary<string, object> {
                    { "@DymbnailID", dymbnailId }
                })
                .Materialize<Variable>();
        }

        public long Create(Variable entity) {
            entity.ID = (long)Procedure("Variables_Create")
                                .WithParameters(new Dictionary<string, object> {
                                    { "@DymbnailID", entity.DymbnailID },
                                    { "@Name", entity.Name },
                                    { "@Title", entity.Title },
                                    { "@Description", entity.Description }
                                }).ExecuteScalar();
            return entity.ID;
        }

        public void Delete(long id) {
            Procedure("Variables_Delete")
                .WithParameters(new Dictionary<string, object> {
                    { "@ID", id }
                }).ExecuteNonQuery();
        }
    }
}
