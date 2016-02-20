namespace ProEvoCanary.Models
{
    public class ResultsModel
    {
        public int ResultId { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeam { get; set; }
        public int AwayScore { get; set; }
        public int EventId { get; set; }
    }
}