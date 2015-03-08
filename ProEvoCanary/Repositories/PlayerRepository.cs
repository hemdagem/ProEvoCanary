using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ProEvoCanary.Helpers.Exceptions;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories
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
            var reader = _helper.ExecuteReader("sp_GetTopPlayers",parameters);
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
                throw new TooManyPlayersReturnedException("Too many players return. A potential issue with the stored procedure sp_GetTopPlayers");
            }

            return players;
        }

        public List<PlayerModel> GetTopPlayers()
        {
            var players = new List<PlayerModel>();
            var reader = _helper.ExecuteReader("sp_GetTopPlayers");
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

        public SelectListModel GetAllPlayers()
        {
            var playerListModel = new SelectListModel { ListItems = new List<SelectListItem>() };
            var reader = _helper.ExecuteReader("sp_GetUsers");
            var players = new List<ListItem>();
            while (reader.Read())
            {
                players.Add(new ListItem
                {
                    Text = reader["Name"].ToString(),
                    Value = reader["Id"].ToString()
                });
            }

            playerListModel.ListItems = new SelectList(players, "Value", "Text");

            return playerListModel;
        }
    }
}