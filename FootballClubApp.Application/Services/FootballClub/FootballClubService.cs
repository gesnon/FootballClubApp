using FootballClubApp.Application.Interfaces;
using FootballClubApp.Application.Models.FootballClub;
using FootballClubApp.Application.Models.FootballPlayer;
using FootballClubApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Services.FootballClubs
{
    public class FootballClubService : IFootballClubService
    {
        private readonly IFootballClubAppContext _context;

        public FootballClubService(IFootballClubAppContext context)
        {
            _context = context;
        }
        public async Task CteateAsync(FootballClubDTO dto)
        {
            FootballClub footballClub = new FootballClub { Name = dto.Name, City = dto.City };
            _context.FootballClubs.Add(footballClub);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task AddPlayerToClubAsync(int playerId, int clubId)
        {
            FootballPlayer player = await _context.FootballPlayers.FirstOrDefaultAsync(_=>_.Id== playerId);
            if(player == null)
            {
                throw new Exception("Игрок не найден");
            }
            FootballClub club = _context.FootballClubs.FirstOrDefault(_ => _.Id == clubId);
            if (player == null)
            {
                throw new Exception("Клуб не найден");
            }

            player.Club = club;

            await _context.SaveChangesAsync(CancellationToken.None);

        }

        public async Task<List<FootballClubGetDTO>> GetAllAsync()
        {
            List<FootballClubGetDTO> result =  await _context.FootballClubs
                .Select(x => new FootballClubGetDTO { City = x.City, Name = x.Name, Id = x.Id }).ToListAsync();
            
            return result;
        
        }
        public async Task<List<FootballClubGetDTO>> GetFansClubAsync(int id)
        {
            List<FanClub> fanClubs = _context.FanClubs.Where(_=>_.FanId==id).ToList();
            List<FootballClubGetDTO> result = new List<FootballClubGetDTO>();

            foreach(FanClub fanClub in fanClubs)
            {
                FootballClub club = _context.FootballClubs.FirstOrDefault(_ => _.Id == fanClub.ClubId);

                result.Add(new FootballClubGetDTO { City = club.City, Name = club.Name, Id = club.Id });
            }
            return result;
        }
        public async Task<List<FootballClubGetDTO>> GetUnfollowedClubs(int FanId)
        {
            List<int?> myClubsIds = _context.FanClubs.Where(_ => _.FanId == FanId).Select(n=> n.ClubId).ToList();

            List<int?> fanClubs = _context.FanClubs.Where(_=> !myClubsIds.Contains(_.ClubId)).Select(_=>_.ClubId).Distinct().ToList();

            List<FootballClub> footballClubs =_context.FootballClubs.ToList();

            List<FootballClubGetDTO> result = new List<FootballClubGetDTO>();

            foreach(int fanClub in fanClubs)
            {
                FootballClub club = footballClubs.FirstOrDefault(_ => _.Id == fanClub);

                result.Add(new FootballClubGetDTO { Id = club.Id, City = club.City, Name = club.Name });
            }

            return result;
        }
        public async Task<FootballClubGetDTO> GetAsync(int id)
        {
            FootballClub footballClub = await _context.FootballClubs.FirstOrDefaultAsync(_ => _.Id == id);
            if (footballClub == null)
            {
                throw new Exception("Клуб не найден");
            }
            List<FootballPlayerGetDTO> players = await _context.FootballPlayers.Where(_=>_.ClubId == id)
                .Select(_=> new FootballPlayerGetDTO { Birth=_.Birth.ToString(), Name=_.FullName, Id=_.Id, SNILS=_.SNILS}).ToListAsync();
            FootballClubGetDTO result = new FootballClubGetDTO
            {
                Name = footballClub.Name,
                City = footballClub.City,
                Id = footballClub.Id,
                FootballPlayers = players
            };

            return result;
        }



        public async Task RemoveAsync(int id)
        {
            FootballClub footballClub =await _context.FootballClubs.FirstOrDefaultAsync(x => x.Id == id);
            if (footballClub == null)
            {
                throw new Exception("Клуб не найден");
            }
            //List<FootballPlayer> players = _context.FootballPlayers.Where(_=>_.ClubId==id).ToList();

            //foreach(FootballPlayer footballPlayer in players)
            //{
            //    footballPlayer.Club = null;
            //}
            List<FanClub> fanClubs = _context.FanClubs.Where(_=>_.ClubId==id).ToList();

            _context.FootballClubs.Remove(footballClub);
            _context.FanClubs.RemoveRange(fanClubs);

            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(FootballClubDTO dto, int id)
        {
            FootballClub footballClub = await _context.FootballClubs.FirstOrDefaultAsync(x => x.Id == id);
            if (footballClub == null)
            {
                throw new Exception("Клуб не найден");
            }

            footballClub.Name = dto.Name;
            footballClub.City = dto.City;

            await _context.SaveChangesAsync(CancellationToken.None);

        }
    }
}
