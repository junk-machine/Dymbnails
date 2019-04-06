using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dymbnails.Logic.Interfaces;
using Autofac;

namespace Dymbnails.Logic {
    public static class Facade {
        private static IContainer container;

        static Facade() {
            container = new ContainerCreator().Create();
        }

        public static IDymbnailsService Dymbnails {
            get { return container.Resolve<IDymbnailsService>(); }
        }

        public static IVariablesService Variables {
            get { return container.Resolve<IVariablesService>(); }
        }

        public static IXVGMLService XVGML {
            get { return container.Resolve<IXVGMLService>(); }
        }
    }
}
