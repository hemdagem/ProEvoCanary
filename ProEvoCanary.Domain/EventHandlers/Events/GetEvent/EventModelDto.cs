﻿using System.Collections.Generic;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.EventHandlers.Events.GetEvent
{
    public class EventModelDto
    {
        public int TournamentId { get; set; }
        public int OwnerId { get; set; }
        public string TournamentName { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public bool FixturesGenerated { get; set; }
        public TournamentType TournamentType { get; set; }
        public List<PlayerModel> Users { get; set; }
        public List<ResultsModel> Results { get; set; }
        public List<Standings> Standings { get; set; }

        public EventModelDto()
        {
            Users = new List<PlayerModel>();
            Results = new List<ResultsModel>();
            Standings = new List<Standings>();
        }
    }
}