using System.Web.Mvc;
using System.Web.Routing;

namespace Dymbnails.Web.Helpers {
    public static class ImageHelper {
        public static string Image(this HtmlHelper helper, string id, string url, string alternateText) {
            return Image(helper, id, url, alternateText, null);
        }

        public static string Image(this HtmlHelper helper, string id, string url, string alternateText, object htmlAttributes) {
            var builder = new TagBuilder("img");

            builder.GenerateId(id);

            builder.MergeAttribute("src", url);
            builder.MergeAttribute("alt", alternateText);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return builder.ToString(TagRenderMode.SelfClosing);
        }
    }
}
