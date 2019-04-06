using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dymbnails.Entities;
using System.Xml.Linq;

namespace Dymbnails.Logic.Interfaces {
    public interface IDymbnailsService {
        List<Dymbnail> GetList();
        Dymbnail Get(int id);
        void Update(Dymbnail entity);

        XElement Transform(int id, IEnumerable<KeyValuePair<string, string>> variables);
    }
}
