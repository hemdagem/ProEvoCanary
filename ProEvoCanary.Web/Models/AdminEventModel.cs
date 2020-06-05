using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEvoCanary.Web.Models
{
	public class AdminEventModel
    {
        public AdminEventModel() { }

        public AdminEventModel(TournamentType tournamentType, string tournamentName, DateTime date, List<PlayerModel> players)
        {
            TournamentType = tournamentType;
            TournamentName = tournamentName;
            Date = date;
            Players = players;
        }


        [Required]
        [Range(1, 3)]
        public TournamentType TournamentType { get; set; }

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