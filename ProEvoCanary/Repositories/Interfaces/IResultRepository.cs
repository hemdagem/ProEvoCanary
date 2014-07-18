using System.Collections.Generic;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IResultRepository
    {
        List<ResultsModel> GetResults();
        List<ResultsModel> GetHeadToHeadResults(int playerOne,int playerTwo);
    }
}