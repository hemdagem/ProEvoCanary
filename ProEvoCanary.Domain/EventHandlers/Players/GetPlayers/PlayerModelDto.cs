namespace ProEvoCanary.Application.EventHandlers.Players.GetPlayers
{
    public class PlayerModelDto
    {
	    public int PlayerId { get; set; }
	    public string PlayerName { get; set; }
	    public float PointsPerGame { get; set; }
	    public float GoalsPerGame { get; set; }
	    public int MatchesPlayed { get; set; }
    }
}