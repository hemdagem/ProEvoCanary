using System.Collections.Generic;
using ProEvo45.Models;

namespace ProEvoCanary.Models
{
    public class ResultsListModel
    {
        public SelectListModel Items { get; set; }
        public List<ResultsModel> Results { get; set; }
    }
}