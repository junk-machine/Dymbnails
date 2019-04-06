using System.Collections.Generic;
using Dymbnails.Entities;
using Dymbnails.DataAccess.Interfaces;

namespace Dymbnails.DataAccess.Components {
    class DymbnailsComponent : ComponentBase, IDymbnailsComponent {
        public DymbnailsComponent(ConnectionManager manager) : base(manager) { }

        public List<Dymbnail> GetList() {
            return Procedure("Dymbnails_GetList").Materialize<Dymbnail>();
        }

        public Dymbnail GetById(int id) {
            return Procedure("Dymbnails_GetByID")
                .WithParameters(new Dictionary<string, object> {
                    { "@ID", id }
                })
                .MaterializeSingle<Dymbnail>();
        }

        public string GetContentById(int id) {
            return (string)Procedure("Dymbnails_GetContentByID")
                            .WithParameters(new Dictionary<string, object> {
                                { "@ID", id }
                            }).ExecuteScalar();
        }

        public void Update(Dymbnail entity) {
            Procedure("Dymbnails_Update")
                .WithParameters(new Dictionary<string, object> {
                    { "@ID", entity.ID },
                    { "@Title", entity.Title },
                    { "@Description", entity.Description },
                    { "@Content", entity.Content }
                }).ExecuteNonQuery();
        }
    }
}
