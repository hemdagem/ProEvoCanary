using System.Collections.Generic;

namespace ProEvoCanary.DataAccess.Models
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