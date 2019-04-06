using System.Collections.Generic;
using Dymbnails.Logic.Interfaces;
using Dymbnails.Entities;
using Dymbnails.DataAccess;
using System.Text.RegularExpressions;

namespace Dymbnails.Logic.Services {
    class VariablesService : ServiceBase, IVariablesService {
        Regex parameterRegex = new Regex(@"<[^>]*?xsl:param[^>]*?name *= *""(?<name>[^""]*)""",
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public List<Variable> GetList(int dymbnailId) {
            using (var database = new Database()) {
                return database.Variables.GetList(dymbnailId);
            }
        }

        public List<string> ExtractNamesFromXslt(string content) {
            var matches = parameterRegex.Matches(content);
            var result = new List<string>(matches.Count);
            foreach (Match match in matches) {
                result.Add(match.Groups["name"].Value);
            }
            return result;
        }
    }
}
