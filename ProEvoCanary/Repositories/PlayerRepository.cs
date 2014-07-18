using System.Collections.Generic;
using System.Web.Mvc;
using ProEvoCanary.Helpers;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDBHelper _helper;

        public List<PlayerModel> GetPlayers()
        {
            var players = new List<PlayerModel>();
            var reader = _helper.ExecuteReader("sp_GetTopPlayers");
            if (reader != null)
            {
                while (reader.Read())
                {
                    players.Add(new PlayerModel
                    {
                        PlayerId = (int)reader["LoginID"],
                        PlayerName = reader["Name"].ToString(),
                        GoalsPerGame = float.Parse(reader["GoalsPerGame"].ToString()),
                        PointsPerGame = float.Parse(reader["PointsPerGame"].ToString()),
                        MatchesPlayed = (int)reader["MatchesPlayed"]
                    });
                }
            }
            return players;
        }

        public SelectListModel GetPlayerList()
        {
            var playerListModel = new SelectListModel { ListItems = new List<SelectListItem>() };
            var reader = _helper.ExecuteReader("sp_GetUsers");

            if (reader != null)
            {
                var players = new List<ListItem>();
                while (reader.Read())
                {
                    players.Add(new ListItem
                    {
                        Text = reader["Name"].ToString(),
                        Value = reader["LoginID"].ToString()
                    });
                }

                playerListModel.ListItems = new SelectList(players, "Value", "Text");
            }
            return playerListModel;
        }


        public PlayerRepository(IDBHelper helper)
        {
            _helper = helper;
        }

        public PlayerRepository()
            : this(new DBHelper())
        {

        }
    }
}