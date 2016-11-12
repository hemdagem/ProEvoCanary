namespace ProEvoCanary.Web.Models
{
    public class Standings
    {
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public string TeamOwner { get; set; }
        public int TournamentId { get; set; }
        public int Won { get; set; }
        public int HomeWon { get; set; }
        public int AwayWon { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int For { get; set; }
        public int Against { get; set; }
        public int GoalDifference { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }
        public int PreviousPosition { get; set; }
    }
}