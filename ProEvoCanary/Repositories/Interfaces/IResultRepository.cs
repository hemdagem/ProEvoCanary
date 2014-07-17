using System.Collections.Generic;
using ProEvo45.Models;

namespace ProEvo45.Repositories.Interfaces
{
    public interface IResultRepository
    {
        List<ResultsModel> GetResults();
    }
}