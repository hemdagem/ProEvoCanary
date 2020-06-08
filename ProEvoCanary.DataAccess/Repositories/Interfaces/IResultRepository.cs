using System;
using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;

namespace ProEvoCanary.DataAccess.Repositories.Interfaces
{
    public interface IResultRepository
    {
        List<ResultsModel> GetResults();
        RecordsModel GetHeadToHeadRecord(int playerOne,int playerTwo);

        int AddResult(Guid id, int homeScore, int awayScore);
        ResultsModel GetResult(Guid id);
    }
}