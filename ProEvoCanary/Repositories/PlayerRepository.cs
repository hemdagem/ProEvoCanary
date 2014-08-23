﻿using System.Collections.Generic;
using System.Web.Mvc;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IdBHelper _helper;

        public PlayerRepository(IdBHelper helper)
        {
            _helper = helper;
        }

        public PlayerRepository() : this(new DBHelper()) { }

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