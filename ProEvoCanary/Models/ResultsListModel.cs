using System.Collections.Generic;

namespace ProEvoCanary.Web.Models
{
    public class ResultsListModel
    {
        public List<PlayerModel> PlayerList { get; set; }
        public int PlayerTwo { get; set; }
        public int PlayerOne { get; set; }
        public RecordsModel HeadToHead { get; set; }
    }
}