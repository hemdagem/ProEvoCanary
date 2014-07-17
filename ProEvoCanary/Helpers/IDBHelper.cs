using System.Data;

namespace ProEvo45.Helpers
{
    public interface IDBHelper
    {
        int ExecuteScalar(string commandText);
        int ExecuteNonQuery(string storedProcedure);
        IDataReader ExecuteReader(string commandText);
    }
}