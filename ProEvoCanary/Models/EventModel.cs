using System.Collections.Generic;

namespace ProEvoCanary.Models
{
    public class EventModel
    {
        public int EventId { get; set; }
        public int OwnerId { get; set; }
        public string EventName { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public bool FixturesGenerated { get; set; }
        public EventTypes EventTypes { get; set; }
        public List<UserModel> Users { get; set; }

        public EventModel()
        {
            Users = new List<UserModel>();
        }
    }
}