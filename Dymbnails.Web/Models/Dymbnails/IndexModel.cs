using System.Collections.Generic;
using Dymbnails.Entities;

namespace Dymbnails.Web.Models.Dymbnails {
    public class IndexModel {
        public List<Dymbnail> Items { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
