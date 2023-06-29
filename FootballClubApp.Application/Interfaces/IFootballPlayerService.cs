using FootballClubApp.Application.Models.FootballPlayer;
using FootballClubApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Interfaces
{
    public interface IFootballPlayerService
    {
        public Task CteateAsync(FootballPlayerDTO dto);
        public Task UpdateAsync(FootballPlayerDTO dto, int id);
        public Task<FootballPlayerGetDTO> GetAsync(int id);

        public Task<List<FootballPlayer>> GetFreePlayersAsync();
        public Task LeavClub(int id);

        public Task<List<FootballPlayerGetDTO>> GetAllAsync();
        public Task RemoveAsync(int id);
    }
}
