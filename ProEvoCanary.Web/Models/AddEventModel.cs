using System;
using System.ComponentModel.DataAnnotations;

namespace ProEvoCanary.Web.Models
{
    public class AddEventModel
    {
        public AddEventModel() { Date = DateTime.Today; }
        public AddEventModel(TournamentType tournamentType, string tournamentName, DateTime date)
        {
            TournamentType = tournamentType;
            TournamentName = tournamentName;
            Date = date;
        }

        [Required]
        [Range(1, 3)]
        public TournamentType TournamentType { get; set; }

        [Required]
        public string TournamentName { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public bool GeneratedFixtures { get; set; }
    }
}