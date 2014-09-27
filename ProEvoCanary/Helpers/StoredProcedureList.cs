using System.Collections.Generic;

namespace ProEvoCanary.Helpers
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
            {StoredProcedureList.GetTournamentDetails, "sp_GetTournamentDetails"},
            {StoredProcedureList.AddTournament, "sp_AddTournament"},
            {StoredProcedureList.GetTopPlayers, "sp_GetTopPlayers"},
            {StoredProcedureList.GetUsers, "sp_GetUsers"},
            {StoredProcedureList.RecentResults, "sp_RecentResults"},
            {StoredProcedureList.HeadToHeadRecord, "sp_HeadToHeadRecord"},
            {StoredProcedureList.LoginDetails, "sp_GetLoginDetails"},
            {StoredProcedureList.AddNewUser, "sp_AddNewUser"}
        };


        public static string Get(StoredProcedureList key)
        {
            return Dictionary[key];
        }

    }
}