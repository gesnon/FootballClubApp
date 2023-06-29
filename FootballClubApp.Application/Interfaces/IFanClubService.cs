using FootballClubApp.Application.Models.FanClub;
using FootballClubApp.Application.Models.FootballFan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Interfaces
{
    public interface IFanClubService
    {
        public Task CreateAsync (FanClubDTO dto);
        public Task RemoveAsync(int id);

        public Task DeleteAsync(int fanId, int clubId);
        
    }
}
