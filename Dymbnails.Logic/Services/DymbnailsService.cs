using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Collections.Generic;
using Dymbnails.Logic.Interfaces;
using Dymbnails.Entities;
using Dymbnails.DataAccess;

namespace Dymbnails.Logic.Services {
    class DymbnailsService : ServiceBase, IDymbnailsService {
        private IVariablesService variablesService;

        public DymbnailsService(IVariablesService variablesService) {
            this.variablesService = variablesService;
        }

        public List<Dymbnail> GetList() {
            using (var database = new Database()) {
                return database.Dymbnails.GetList();
            }
        }

        public Dymbnail Get(int id) {
            using (var database = new Database()) {
                return database.Dymbnails.GetById(id);
            }
        }

        public void Update(Dymbnail entity) {
            using (var database = new Database()) {
                var variableNames = variablesService.ExtractNamesFromXslt(entity.Content);

                database.BeginTransaction();
                // Update variables
                var oldVariables = variablesService.GetList(entity.ID);
                var variables = new List<Variable>(variableNames.Count);
                foreach (var name in variableNames) {
                    var newVariable = oldVariables.FirstOrDefault(variable => variable.Name == name);
                    if (newVariable == null) {
                        newVariable = new Variable { DymbnailID = entity.ID, Name = name, Title = name };
                    }
                    variables.Add(newVariable);
                }
                var variablesToDelete = oldVariables.Except(variables).Select(variable => variable.ID);
                foreach (var id in variablesToDelete) {
                    database.Variables.Delete(id);
                }
                foreach (var variable in variables) {
                    if (variable.ID > 0) continue;
                    database.Variables.Create(variable);
                }
                
                // Update entity
                database.Dymbnails.Update(entity);
                database.CommitTransaction();
            }
        }

        public XElement Transform(int id, IEnumerable<KeyValuePair<string, string>> variables) {
            var xslt = GetCompiledXslt(id);
            if (xslt == null) return null;
            
            using (var stream = new MemoryStream()) {
                var writer = XmlWriter.Create(stream, xslt.OutputSettings);
                xslt.Transform(new XmlDocument(), BuildXsltArgumentsList(variables), writer);
                writer.Close();
                stream.Position = 0;
                return XElement.Load(XmlTextReader.Create(stream));
            }
        }

        private XslCompiledTransform GetCompiledXslt(int id) {
            string content;
            using (var database = new Database()) {
                content = database.Dymbnails.GetContentById(id);
            }
            if (string.IsNullOrEmpty(content)) return null;

            var xslt = new XslCompiledTransform();
            using (var reader = new StringReader(content)) {
                xslt.Load(new XPathDocument(reader),
                            new XsltSettings(true, false), new XmlUrlResolver());
            }
            return xslt;
        }

        private XsltArgumentList BuildXsltArgumentsList(IEnumerable<KeyValuePair<string, string>> variables) {
            var result = new XsltArgumentList();
            foreach (var variable in variables) {
                result.AddParam(variable.Key, string.Empty, variable.Value);
            }
            return result;
        }
    }
}
