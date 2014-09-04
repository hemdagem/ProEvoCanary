using System.Collections.Generic;

namespace ProEvoCanary.Models
{
    public class ResultsListModel
    {
        public SelectListModel PlayerOneList { get; set; }
        public SelectListModel PlayerTwoList { get; set; }
        public RecordsModel HeadToHead { get; set; }
    }
}