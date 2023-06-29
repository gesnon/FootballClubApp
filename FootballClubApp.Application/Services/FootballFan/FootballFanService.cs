using FootballClubApp.Application.Interfaces;
using FootballClubApp.Application.Models.FootballClub;
using FootballClubApp.Application.Models.FootballFan;
using FootballClubApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Services.FootballFans
{
    public class FootballFanService : IFootballFanService
    {

        private readonly IFootballClubAppContext _context;

        public FootballFanService(IFootballClubAppContext context)
        {
            _context = context;
        }

        public async Task CteateAsync(FootballFanDTO dto)
        {
            FootballFan footballFan = new FootballFan { FullName = dto.Name };
            _context.FootballFans.Add(footballFan);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
        public async Task<List<FootballFanGetDTO>> GetAllAsync()
        {
            List<FootballFanGetDTO> result = await _context.FootballFans
                .Select(x => new FootballFanGetDTO { Id = x.Id, Name = x.FullName }).ToListAsync();

            return result;
        }

        public async Task<FootballFanGetDTO> GetAsync(int id)
        {
            FootballFan footballFan = await _context.FootballFans.FirstOrDefaultAsync(_ => _.Id == id);
            if (footballFan == null)
            {
                throw new Exception("Болельщик не найден");
            }
            FootballFanGetDTO result = new FootballFanGetDTO
            {
                Name = footballFan.FullName,
                Id = footballFan.Id,
                FollowedClubDTOs = new List<FootballClubPreviewDTO>()
            };
            //List<FootballClubPreviewDTO> fanClubs = new List<FootballClubPreviewDTO>();
            List<FanClub> clubs = _context.FanClubs.Where(x => x.FanId == footballFan.Id).ToList();
            foreach (var club in clubs)
            {
                string clubName = _context.FootballClubs.FirstOrDefault(_ => _.Id == club.ClubId).Name;
                string city = _context.FootballClubs.FirstOrDefault(_ => _.Id == club.ClubId).City;
                result.FollowedClubDTOs.Add(new FootballClubPreviewDTO { Name = clubName, Id = club.ClubId, City= city});
            }

            return result;

        }

        public async Task RemoveAsync(int id)
        {
            FootballFan footballFan = await _context.FootballFans.FirstOrDefaultAsync(_ => _.Id == id);
            if (footballFan == null)
            {
                throw new Exception("Болельщик не найден");
            }
            _context.FootballFans.Remove(footballFan);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(FootballFanDTO dto, int id)
        {
            FootballFan footballFan = await _context.FootballFans.FirstOrDefaultAsync(_ => _.Id == id);
            if (footballFan == null)
            {
                throw new Exception("Болельщик не найден");
            }
            footballFan.FullName = dto.Name;
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
