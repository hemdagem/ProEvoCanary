using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IdBHelper _helper;

        public EventRepository(IdBHelper helper)
        {
            _helper = helper;
        }

        public EventRepository() : this(new DBHelper()) { }

        public List<EventModel> GetEvents()
        {
            var reader = _helper.ExecuteReader("sp_GetTournamentDetails");
            var lstTournament = new List<EventModel>();
            while (reader.Read())
            {
                lstTournament.Add(new EventModel
                {
                    EventId = (int)reader["Id"],
                    EventName = reader["TournamentName"].ToString(),
                    Venue = reader["Venue"].ToString(),
                    Date = reader["Date"].ToString(),
                    Name = reader["Name"].ToString(),
                    Completed = (bool)reader["Completed"]
                });

            }
            return lstTournament;
        }



    }
}