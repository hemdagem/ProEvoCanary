using System.Collections.Generic;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IResultRepository
    {
        List<ResultsModel> GetResults();
        RecordsModel GetHeadToHeadRecord(int playerOne,int playerTwo);
    }
}