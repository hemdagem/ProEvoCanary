using System.Data;

namespace ProEvoCanary.Helpers.Interfaces
{
    public interface IDBHelper
    {
        int ExecuteScalar(string commandText);
        int ExecuteNonQuery(string storedProcedure);
        IDataReader ExecuteReader(string commandText);
        void AddParameter(string parameterName, object value);
        void ClearParameters();
    }
}