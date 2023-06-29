using FootballClubApp.Application.Models.FootballFan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Interfaces
{
    public interface IFootballFanService
    {
        public Task CteateAsync(FootballFanDTO dto);
        public Task UpdateAsync(FootballFanDTO dto, int id);
        public Task<FootballFanGetDTO> GetAsync(int id);

        public Task<List<FootballFanGetDTO>> GetAllAsync();
        public Task RemoveAsync(int id);
    }
}
