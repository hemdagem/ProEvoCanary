using System.Collections.Generic;

namespace ProEvoCanary.Domain.Helpers
{
    public enum StoredProcedureList
    {
        GetTournamentDetails,
        AddTournament,
        GetTopPlayers,
        GetUsers,
        RecentResults,
        HeadToHeadRecord,
        LoginDetails,
        AddNewUser

    }

    public class StoredProcedures
    {

        private static readonly Dictionary<StoredProcedureList, string> Dictionary = new Dictionary<StoredProcedureList, string>
        {
            {StoredProcedureList.GetTournamentDetails, "up_GetTournamentDetails"},
            {StoredProcedureList.AddTournament, "up_AddTournament"},
            {StoredProcedureList.GetTopPlayers, "up_GetTopPlayers"},
            {StoredProcedureList.GetUsers, "up_GetUsers"},
            {StoredProcedureList.RecentResults, "up_RecentResults"},
            {StoredProcedureList.HeadToHeadRecord, "up_HeadToHeadRecord"},
            {StoredProcedureList.LoginDetails, "up_GetLoginDetails"},
            {StoredProcedureList.AddNewUser, "up_AddUser"}
        };


        public static string Get(StoredProcedureList key)
        {
            return Dictionary[key];
        }

    }
}