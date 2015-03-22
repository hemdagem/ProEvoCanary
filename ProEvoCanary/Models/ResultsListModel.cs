using System.Collections.Generic;

namespace ProEvoCanary.Models
{
    public class ResultsListModel
    {
        public List<PlayerModel> PlayerOneList { get; set; }
        public List<PlayerModel> PlayerTwoList { get; set; }
        public int PlayerTwo { get; set; }
        public int PlayerOne { get; set; }
        public RecordsModel HeadToHead { get; set; }
    }
}