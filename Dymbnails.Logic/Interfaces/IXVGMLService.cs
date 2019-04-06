using System.IO;
using System.Drawing;
using System.Xml.Linq;

namespace Dymbnails.Logic.Interfaces {
    public interface IXVGMLService {
        void Render(XElement imageXml, Stream output);
    }
}
