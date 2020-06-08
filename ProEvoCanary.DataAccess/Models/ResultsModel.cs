using System;

namespace ProEvoCanary.DataAccess.Models
{
    public class ResultsModel
    {
        public Guid ResultId { get; set; }
        public Guid TournamentId { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeam { get; set; }
        public int AwayScore { get; set; }
    }
}