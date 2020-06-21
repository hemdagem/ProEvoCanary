using System;
using System.Collections.Generic;
using ProEvoCanary.Application.EventHandlers.Players.GetPlayers;

namespace ProEvoCanary.Application.EventHandlers.Events.Queries
{
    public class EventModelDto
    {
        public Guid TournamentId { get; set; }
        public int OwnerId { get; set; }
        public string TournamentName { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public bool FixturesGenerated { get; set; }
        public TournamentType TournamentType { get; set; }
        public List<PlayerModelDto> Users { get; set; }
        public List<ResultsModel> Results { get; set; }
        public List<Standings> Standings { get; set; }

        public EventModelDto()
        {
            Users = new List<PlayerModelDto>();
            Results = new List<ResultsModel>();
            Standings = new List<Standings>();
        }
    }
}