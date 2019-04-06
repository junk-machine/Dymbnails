using System.Collections.Generic;
using Dymbnails.Entities;

namespace Dymbnails.Web.Models.Dymbnails {
    public class GetUrlModel {
        public int DymbnailId { get; set; }
        public List<Variable> Variables { get; set; }
    }
}
