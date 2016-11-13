using System.Collections.Generic;

namespace ProEvoCanary.Web.Models
{
    public class RecordsModel
    {
        public int TotalMatches { get; set; }
        public int TotalDraws { get; set; }
        public int PlayerOneWins { get; set; }
        public int PlayerTwoWins { get; set; }
        public List<ResultsModel> Results { get; set; }
    }
}