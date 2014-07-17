using System.Collections.Generic;
using ProEvo45.Helpers;
using ProEvo45.Models;
using ProEvo45.Repositories.Interfaces;

namespace ProEvo45.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IDBHelper _helper;

        public List<EventModel> GetEvents()
        {

            var reader = _helper.ExecuteReader("[sp_GetTDetails]");
            var lstTournament = new List<EventModel>();
            if (reader != null)
            {

                while (reader.Read())
                {
                    lstTournament.Add(new EventModel
                    {
                        EventID = (int)reader["TournamentID"],
                        EventName = reader["TournamentName"].ToString(),
                        Venue = reader["Venue"].ToString(),
                        Date = reader["Date"].ToString(),
                        Name = reader["Name"].ToString(),
                        Completed = (bool)reader["Completed"]
                    });
                   
                }
            }

            return lstTournament;
        }


        public EventRepository(IDBHelper helper)
        {
            _helper = helper;
        }

        public EventRepository()
            : this(new DBHelper())
        {

        }
    }
}