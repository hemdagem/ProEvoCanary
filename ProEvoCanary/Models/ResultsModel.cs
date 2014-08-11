namespace ProEvoCanary.Models
{
    public class ResultsModel
    {
        public int ResultID { get; set; }
        public int HomeTeamID { get; set; }
        public string HomeTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayTeamID { get; set; }
        public string AwayTeam { get; set; }
        public int AwayScore { get; set; }
    }
}