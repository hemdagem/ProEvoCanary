using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace ProEvoCanary.Helpers
{
    public interface IDBHelper
    {
        int ExecuteScalar(string commandText);
        int ExecuteNonQuery(string storedProcedure);
        IDataReader ExecuteReader(string commandText);
        void AddParameter(string parameterName, object value);
    }
}