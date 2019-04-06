using System.Web;
using System.Text;
using System.Configuration;

namespace Dymbnails.Web.Helpers {
    public static class DymbnailsHelper {
        public const string DefaultContentTemplate = @"
<?xml version=""1.0\"" encoding=""utf-8""?>
<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
  <xsl:output method=""xml"" encoding=""utf-8"" />
  <xsl:template match=""/"">
    <basic:Canvas xmlns:basic=""basic"" xmlns:web=""web""
                  Size=""120 60"" Background=""White"">
      <!-- Place your drawing here -->
    </basic:Canvas>
  </xsl:template>
</xsl:stylesheet>";

        public static string GetUrl(int id) {
            return ConfigurationManager.AppSettings["DymbnailsServerUrl"] + id;
        }

        public static string GetUrl(int id, params object[] parameters) {
            var parametersString = new StringBuilder();
            for (int index = 0; index < parameters.Length; index += 2) {
                parametersString.Append(parameters[index]);
                parametersString.Append('=');
                parametersString.Append(HttpUtility.UrlEncode(parameters[index + 1].ToString()));
                parametersString.Append('&');
            }
            parametersString.Remove(parametersString.Length - 1, 1);
            return GetUrl(id) + "?" + parametersString.ToString();
        }
    }
}
