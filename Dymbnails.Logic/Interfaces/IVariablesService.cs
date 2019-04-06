using System.Collections.Generic;
using Dymbnails.Entities;

namespace Dymbnails.Logic.Interfaces {
    public interface IVariablesService {
        List<Variable> GetList(int dymbnailId);
        List<string> ExtractNamesFromXslt(string content);
    }
}
