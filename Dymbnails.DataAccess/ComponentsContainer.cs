using Autofac;
using Dymbnails.DataAccess.Components;
using Dymbnails.DataAccess.Interfaces;

namespace Dymbnails.DataAccess {
    static class ComponentsContainer {
        private static IContainer container;

        static ComponentsContainer() {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConnectionManager>().InstancePerLifetimeScope();
            builder.RegisterType<DymbnailsComponent>().As<IDymbnailsComponent>().InstancePerLifetimeScope();
            builder.RegisterType<VariablesComponent>().As<IVariablesComponent>().InstancePerLifetimeScope();

            container = builder.Build();
        }

        public static ILifetimeScope CreateScope() {
            return container.BeginLifetimeScope();
        }
    }
}
