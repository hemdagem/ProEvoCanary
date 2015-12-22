using ProEvoCanary.Models;

namespace ProEvoCanary.ModelBuilders
{
    public interface IPlayerModelBuilder
    {
        PlayerModel BuildViewModel(Domain.Models.PlayerModel domainPlayerModel);
        Domain.Models.PlayerModel BuildCoreModel(PlayerModel playerViewModel);
    }

    public class PlayerModelBuilder
    {
        public PlayerModel BuildViewModel(Domain.Models.PlayerModel domainPlayerModel)
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

        public Domain.Models.PlayerModel BuildCoreModel(PlayerModel playerViewModel)
        {
            return new Domain.Models.PlayerModel
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