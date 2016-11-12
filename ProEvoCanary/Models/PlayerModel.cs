namespace ProEvoCanary.Web.Models
{
    public class PlayerModel
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public float PointsPerGame { get; set; }
        public float GoalsPerGame { get; set; }
        public int MatchesPlayed { get; set; }
    }
}