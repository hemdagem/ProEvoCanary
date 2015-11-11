using System.Collections.Generic;
using ProEvoCanary.Domain;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IResultRepository
    {
        List<ResultsModel> GetResults();
        RecordsModel GetHeadToHeadRecord(int playerOne,int playerTwo);
    }
}