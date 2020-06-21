using System;
using System.Collections.Generic;

namespace ProEvoCanary.Web.Models
{
    public class FixturesModel
    {
        public Guid TournamentId { get; set; }
        public int OwnerId { get; set; }
        public string TournamentName { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public bool FixturesGenerated { get; set; }
        public TournamentType TournamentType { get; set; }
        public List<PlayerModel> Users { get; set; }

        public FixturesModel()
        {
            Users = new List<PlayerModel>();
        }
    }
}