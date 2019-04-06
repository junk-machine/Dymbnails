using System.Data.SqlClient;

namespace Dymbnails.DataAccess.Components {
    abstract class ComponentBase {
        protected ConnectionManager ConnectionManager { get; private set; }

        public ComponentBase(ConnectionManager manager) {
            ConnectionManager = manager;
        }

        public SqlCommand Procedure(string name) {
            var command = ConnectionManager.Connection.Call(name);
            return ConnectionManager.Transaction == null 
                ? command
                : command.InTransaction(ConnectionManager.Transaction);
        }
    }
}
