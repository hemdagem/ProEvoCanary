using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web.Mvc;
using ProEvoCanary.Helpers;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDBHelper _helper;
        private readonly MemoryCache _memoryCache;
        private const string TopPlayerListCacheKey = "TopPlayerCacheList";
        private const string PlayerListCacheKey = "PlayerCacheList";
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(3)
        };

        public List<PlayerModel> GetPlayers()
        {
            if (_memoryCache.Contains(TopPlayerListCacheKey))
            {
                return _memoryCache.Get(TopPlayerListCacheKey) as List<PlayerModel>;
            }
            var players = new List<PlayerModel>();
            var reader = _helper.ExecuteReader("sp_GetTopPlayers");
            if (reader != null)
            {
                while (reader.Read())
                {
                    players.Add(new PlayerModel
                    {
                        PlayerId = (int)reader["UserId"],
                        PlayerName = reader["Name"].ToString(),
                        GoalsPerGame = float.Parse(reader["GoalsPerGame"].ToString()),
                        PointsPerGame = float.Parse(reader["PointsPerGame"].ToString()),
                        MatchesPlayed = (int)reader["MatchesPlayed"]
                    });
                }
            }
            _memoryCache.Add(TopPlayerListCacheKey, players, _policy);
            return players;
        }

        public SelectListModel GetPlayerList()
        {
            if (_memoryCache.Contains(PlayerListCacheKey))
            {
                return _memoryCache.Get(PlayerListCacheKey) as SelectListModel;
            }
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
                        Value = reader["UserId"].ToString()
                    });
                }

                playerListModel.ListItems = new SelectList(players, "Value", "Text");
            }
            _memoryCache.Add(PlayerListCacheKey, playerListModel, _policy);
            return playerListModel;
        }


        public PlayerRepository(IDBHelper helper, MemoryCache memoryCache)
        {
            _helper = helper;
            _memoryCache = memoryCache;
        }

        public PlayerRepository() : this(new DBHelper(), MemoryCache.Default)
        {

        }
    }
}