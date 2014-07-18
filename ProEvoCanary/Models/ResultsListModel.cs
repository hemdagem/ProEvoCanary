using System.Collections.Generic;

namespace ProEvoCanary.Models
{
    public class ResultsListModel
    {
        public SelectListModel Items { get; set; }
        public List<ResultsModel> Results { get; set; }
    }
}