using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Interfaces
{
    public interface IDataService
    {
        Task FillData();

        Task RemoveData();
    }
}
