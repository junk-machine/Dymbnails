using System;
using System.Data.SqlClient;

namespace Dymbnails.DataAccess {
    class ConnectionManager : IDisposable {
        public SqlConnection Connection { get; private set; }
        public SqlTransaction Transaction { get; private set; }

        public ConnectionManager() {
            Connection = new SqlConnection(ConnectionString.Current);
            Connection.Open();
        }

        public void BeginTransaction() {
            Transaction = Connection.BeginTransaction();
        }

        public void CommitTransaction() {
            Transaction.Commit();
            Transaction.Dispose();
            Transaction = null;
        }

        public void RollbackTransaction() {
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
        }

        #region Disposing

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ConnectionManager() {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (Transaction != null) {
                    Transaction.Rollback();
                    Transaction.Dispose();
                    Transaction = null;
                }
                if (Connection != null) {
                    Connection.Dispose();
                    Connection = null;
                }
            }
        }

        #endregion Disposing
    }
}
