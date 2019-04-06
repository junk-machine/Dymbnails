using System.Configuration;

namespace Dymbnails.DataAccess {
    public class ConnectionString {
        public static string Current {
            get {
                return ConfigurationManager.ConnectionStrings["DymbnailsDatabase"].ConnectionString;
            }
        }
    }
}
