using System;
using System.Data.SqlClient;
using Autofac;
using Dymbnails.DataAccess.Interfaces;
using Dymbnails.DataAccess.Components;


namespace Dymbnails.DataAccess {
    public class Database : IDisposable {
        private ILifetimeScope componentsScope;

        public Database() {
            componentsScope = ComponentsContainer.CreateScope();
        }

        private ConnectionManager ConnectionManager {
            get { return componentsScope.Resolve<ConnectionManager>(); }
        }

        public void BeginTransaction() {
            ConnectionManager.BeginTransaction();
        }

        public void CommitTransaction() {
            ConnectionManager.CommitTransaction();
        }

        public void RollbackTransaction() {
            ConnectionManager.RollbackTransaction();
        }

        #region Components

        public IDymbnailsComponent Dymbnails {
            get { return componentsScope.Resolve<IDymbnailsComponent>(); }
        }

        public IVariablesComponent Variables {
            get { return componentsScope.Resolve<IVariablesComponent>(); }
        }

        #endregion Components

        #region Disposing

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Database() {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (componentsScope != null) {
                    componentsScope.Dispose();
                    componentsScope = null;
                }
            }
        }

        #endregion Disposing
    }
}
