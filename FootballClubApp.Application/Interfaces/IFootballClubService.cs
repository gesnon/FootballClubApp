using FootballClubApp.Application.Models.FootballClub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Interfaces
{
    public interface IFootballClubService
    {
        public Task CteateAsync(FootballClubDTO dto);
        public Task UpdateAsync(FootballClubDTO dto, int id);
        public Task<FootballClubGetDTO> GetAsync(int id);
        public Task<List<FootballClubGetDTO>> GetAllAsync();
        public Task<List<FootballClubGetDTO>> GetUnfollowedClubs(int Id);
        public Task<List<FootballClubGetDTO>> GetFansClubAsync(int id);
        public Task AddPlayerToClubAsync(int playerId, int clubId);
        public Task RemoveAsync(int id);
    }
}
