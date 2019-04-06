using Autofac;
using Dymbnails.Logic.Interfaces;
using Dymbnails.Logic.Services;

namespace Dymbnails.Logic {
    class ContainerCreator {
        public IContainer Create() {
            var builder = new ContainerBuilder();

            builder.RegisterType<DymbnailsService>().As<IDymbnailsService>().SingleInstance();
            builder.RegisterType<VariablesService>().As<IVariablesService>().SingleInstance();
            builder.RegisterType<XVGMLService>().As<IXVGMLService>().SingleInstance();

            return builder.Build();
        }
    }
}
