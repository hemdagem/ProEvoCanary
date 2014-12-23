using System;
using System.ComponentModel.DataAnnotations;
namespace ProEvoCanary.Models
{
    public class AddEventModel
    {
        public AddEventModel() { }
        public AddEventModel(EventTypes eventType, string tournamentName, DateTime date)
        {
            EventType = eventType;
            TournamentName = tournamentName;
            Date = date;
        }

        [Required]
        [Range(1, 3)]
        public EventTypes? EventType { get; set; }

        [Required]
        public string TournamentName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public bool GeneratedFixtures { get; set; }
    }
}