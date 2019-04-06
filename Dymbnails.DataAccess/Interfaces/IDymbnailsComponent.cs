using System.Collections.Generic;
using Dymbnails.Entities;

namespace Dymbnails.DataAccess.Interfaces {
    public interface IDymbnailsComponent {
        List<Dymbnail> GetList();
        Dymbnail GetById(int id);
        string GetContentById(int id);
        void Update(Dymbnail entity);
    }
}
