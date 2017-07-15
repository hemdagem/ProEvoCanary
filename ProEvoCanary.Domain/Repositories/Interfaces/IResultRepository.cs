using System.Collections.Generic;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Repositories.Interfaces
{
    public interface IResultRepository
    {
        List<ResultsModel> GetResults();
        RecordsModel GetHeadToHeadRecord(int playerOne,int playerTwo);

        int AddResult(int id, int homeScore, int awayScore);
        ResultsModel GetResult(int id);
    }
}