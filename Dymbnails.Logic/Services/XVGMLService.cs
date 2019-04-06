using System.IO;
using System.Xml.Linq;
using System.Drawing.Imaging;
using XVGML;
using Dymbnails.Logic.Interfaces;

namespace Dymbnails.Logic.Services {
    class XVGMLService : IXVGMLService {
        private LayoutBuilder builder;
        private LayoutRenderer renderer;

        public XVGMLService() {
            builder = new LayoutBuilder();
            builder.RegisterPackage(new XVGML.Basic.PackageDescriptor());
            builder.RegisterPackage(new XVGML.Web.PackageDescriptor());
            renderer = new LayoutRenderer();
        }

        public void Render(XElement imageXml, Stream output) {
            var image = renderer.Render(builder.Build(imageXml));
            image.Save(output, ImageFormat.Png);
        }
    }
}
