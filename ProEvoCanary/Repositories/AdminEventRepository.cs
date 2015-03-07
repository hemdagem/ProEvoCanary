﻿using System;
using System.Collections.Generic;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Exceptions;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;
using EventModel = ProEvoCanary.Models.EventModel;

namespace ProEvoCanary.Repositories
{
    public class AdminEventRepository : IAdminEventRepository
    {
        private readonly IDBHelper _helper;

        public AdminEventRepository(IDBHelper helper)
        {
            _helper = helper;
        }

        public List<EventModel> GetEvents()
        {
            _helper.ClearParameters();
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

        public EventModel GetEvent(int id)
        {
            throw new NotImplementedException();
        }

        public int CreateEvent(string tournamentname, DateTime utcNow, EventTypes? eventType, int ownerId)
        {
            if (string.IsNullOrEmpty(tournamentname))
            {
                throw new NullReferenceException("Tournament Name is null or empty");
            }

            if (ownerId < 1)
            {
                throw new LessThanOneException("Owner Id must be greater than zero");
            }
            _helper.ClearParameters();
            _helper.AddParameter("@TournamentName", tournamentname);
            _helper.AddParameter("@TournamentType", (int)eventType);
            _helper.AddParameter("@Date", utcNow);
            _helper.AddParameter("@OwnerId", ownerId);

            return _helper.ExecuteScalar("sp_AddTournament");
        }
    }
}