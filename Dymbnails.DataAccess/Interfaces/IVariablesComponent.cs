using System.Collections.Generic;
using Dymbnails.Entities;

namespace Dymbnails.DataAccess.Interfaces {
    public interface IVariablesComponent {
        List<Variable> GetList(int dymbnailId);
        long Create(Variable entity);
        void Delete(long id);
    }
}
