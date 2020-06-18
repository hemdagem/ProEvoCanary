namespace ProEvoCanary.Domain.EventHandlers.Events.Queries
{
    public class ResultsModel
    {
        public int ResultId { get; set; }
        public int TournamentId { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeam { get; set; }
        public int AwayScore { get; set; }
    }
}