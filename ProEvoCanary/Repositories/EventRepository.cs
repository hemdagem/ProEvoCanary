using System;
using System.Collections.Generic;
using ProEvoCanary.Areas.Admin.Models;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Exceptions;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories.Interfaces;
using EventModel = ProEvoCanary.Models.EventModel;

namespace ProEvoCanary.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IDBHelper _helper;

        public EventRepository(IDBHelper helper)
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
                    Date = reader["Date"].ToString(),
                    Name = reader["Name"].ToString(),
                    Completed = (bool)reader["Completed"]
                });

            }
            return lstTournament;
        }

    }
}