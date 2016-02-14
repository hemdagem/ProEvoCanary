using System;
using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers.Exceptions;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDBHelper _helper;

        public PlayerRepository(IDBHelper helper)
        {
            _helper = helper;
        }

        public List<PlayerModel> GetTopPlayersRange(int pageNumber = 1, int playersPerPage = 10)
        {
            if (pageNumber < 1 || playersPerPage < 1)
            {
                throw new LessThanOneException();
            }

            var parameters = new Dictionary<string, IConvertible>
            {
                { "@RowsPerPage", playersPerPage },
                { "@PageNumber", pageNumber },
            };

            var players = new List<PlayerModel>();
            var reader = _helper.ExecuteReader("up_GetTopPlayers",parameters);
            while (reader.Read())
            {
                players.Add(new PlayerModel
                {
                    PlayerId = (int)reader["Id"],
                    PlayerName = reader["Name"].ToString(),
                    GoalsPerGame = float.Parse(reader["GoalsPerGame"].ToString()),
                    PointsPerGame = float.Parse(reader["PointsPerGame"].ToString()),
                    MatchesPlayed = (int)reader["MatchesPlayed"]
                });
            }

            if (players.Count > playersPerPage)
            {
                throw new TooManyPlayersReturnedException("Too many players return. A potential issue with the stored procedure up_GetTopPlayers");
            }

            return players;
        }

        public List<PlayerModel> GetTopPlayers()
        {
            var players = new List<PlayerModel>();
            var reader = _helper.ExecuteReader("up_GetTopPlayers");
            while (reader.Read())
            {
                players.Add(new PlayerModel
                {
                    PlayerId = (int)reader["Id"],
                    PlayerName = reader["Name"].ToString(),
                    GoalsPerGame = float.Parse(reader["GoalsPerGame"].ToString()),
                    PointsPerGame = float.Parse(reader["PointsPerGame"].ToString()),
                    MatchesPlayed = (int)reader["MatchesPlayed"]
                });
            }

            return players;
        }

        public List<PlayerModel> GetAllPlayers()
        {
            var players = new List<PlayerModel>();
            var reader = _helper.ExecuteReader("up_GetUsers");

            while (reader.Read())
            {
                players.Add(new PlayerModel
                {
                    PlayerName = reader["Name"].ToString(),
                    PlayerId = (int)reader["Id"]
                });
            }

            return players;
        }
    }
}