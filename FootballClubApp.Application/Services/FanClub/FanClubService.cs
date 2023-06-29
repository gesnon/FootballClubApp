using FootballClubApp.Application.Interfaces;
using FootballClubApp.Application.Models.FanClub;
using FootballClubApp.Application.Models.FootballFan;
using FootballClubApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Services.FanClubs
{
    public class FanClubService : IFanClubService
    {
        private readonly IFootballClubAppContext _context;

        public FanClubService(IFootballClubAppContext context)
        {
            _context = context;
        }


        public async Task CreateAsync(FanClubDTO dto)
        {
            FanClub fanClub = new FanClub { ClubId = dto.ClubId, FanId = dto.FanId };

            _context.FanClubs.Add(fanClub);

            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(int fanId, int clubId)
        {
            FanClub fanClub = await _context.FanClubs.FirstOrDefaultAsync(_=>_.FanId==fanId && _.ClubId==clubId);

            if (fanClub == null)
            {
                throw new Exception("Клуб не найден");
            }

            _context.FanClubs.Remove(fanClub);

            await _context.SaveChangesAsync(CancellationToken.None);

        }

        public async Task RemoveAsync(int id)
        {
            FanClub fanClub =await _context.FanClubs.FirstOrDefaultAsync(x => x.ClubId == id);
            if(fanClub == null)
            {
                throw new Exception("Клуб не найден");
            }

            _context.FanClubs.Remove(fanClub);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
