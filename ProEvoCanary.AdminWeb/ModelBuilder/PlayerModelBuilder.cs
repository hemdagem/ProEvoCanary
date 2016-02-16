using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.AdminWeb.ModelBuilder
{
    public interface IPlayerModelBuilder
    {
        PlayerModel BuildViewModel(PlayerModel domainPlayerModel);
        PlayerModel BuildCoreModel(PlayerModel playerViewModel);
    }

    public class PlayerModelBuilder
    {
        public PlayerModel BuildViewModel(PlayerModel domainPlayerModel)
        {
            return new PlayerModel
            {
                GoalsPerGame = domainPlayerModel.GoalsPerGame,
                MatchesPlayed = domainPlayerModel.MatchesPlayed,
                PlayerId = domainPlayerModel.PlayerId,
                PlayerName = domainPlayerModel.PlayerName,
                PointsPerGame = domainPlayerModel.PointsPerGame
            };
        }

        public PlayerModel BuildCoreModel(PlayerModel playerViewModel)
        {
            return new PlayerModel
            {
                GoalsPerGame = playerViewModel.GoalsPerGame,
                MatchesPlayed = playerViewModel.MatchesPlayed,
                PlayerId = playerViewModel.PlayerId,
                PlayerName = playerViewModel.PlayerName,
                PointsPerGame = playerViewModel.PointsPerGame
            };
        }
    }
}