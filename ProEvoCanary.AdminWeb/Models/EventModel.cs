using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.AdminWeb.Models
{
    public class EventModel
    {
        public EventModel() { }

        public EventModel(EventTypes eventType, string tournamentName, DateTime date, List<PlayerModel> players)
        {
            EventType = eventType;
            TournamentName = tournamentName;
            Date = date;
            Players = players;
        }


        [Required]
        [Range(1, 3)]
        public EventTypes EventType { get; set; }

        [Required]
        public string TournamentName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public List<PlayerModel> Players { get; set; }

        [Required]
        public int OwnerId { get; set; }

    }
}