using SBC.TimeCards.Service.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Interfaces
{
    public interface ISearchService
    {
        SearchResultsViewModel Search(string searchString);
    }
}
