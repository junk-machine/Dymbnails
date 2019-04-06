using System.IO;
using System.Collections.Generic;
using System.Web.Mvc;
using Dymbnails.Logic;

namespace Dymbnails.WebService.Controllers {
    public class MainController : Controller {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Render(int id) {
            var imageXml = Facade.Dymbnails.Transform(id, GetVariablesFromQuery());
            if (imageXml == null) return new EmptyResult();
            var stream = new MemoryStream();
            Facade.XVGML.Render(imageXml, stream);
            stream.Position = 0;
            return new FileStreamResult(stream, "image/png");
        }

        private IEnumerable<KeyValuePair<string, string>> GetVariablesFromQuery() {
            foreach (var key in Request.QueryString.AllKeys) {
                yield return new KeyValuePair<string, string>(key, Server.UrlDecode(Request.QueryString[key]));
            }
        }
    }
}
