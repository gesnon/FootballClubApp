using FootballClubApp.Application.Interfaces;
using FootballClubApp.Application.Models.FootballPlayer;
using FootballClubApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Services.FootballPlayers
{
    public class FootballPlayerService : IFootballPlayerService
    {
        private readonly IFootballClubAppContext _context;

        public FootballPlayerService(IFootballClubAppContext context)
        {
            _context = context;
        }
        public async Task<List<FootballPlayerGetDTO>> GetAllAsync()
        {
            List<FootballPlayerGetDTO> result = await _context.FootballPlayers
                .Select(x => new FootballPlayerGetDTO
                {
                    Id = x.Id,
                    Name = x.FullName,
                    ClubId = x.ClubId,
                    Birth = x.Birth.ToString(),
                    SNILS = x.SNILS,
                    Club = ""

                }).ToListAsync();

            foreach (var Player in result)
            {
                if (Player.Club == null)
                {
                    Player.Club = "";
                }
                if (Player.ClubId == 0)
                {
                    continue;
                }
                FootballClub club = _context.FootballClubs.FirstOrDefault(_ => _.Id == Player.ClubId);
                if (club == null)
                {
                    continue;
                }
                Player.Club = club.Name;
            }
            return result;
        }

        public async Task<List<FootballPlayer>> GetFreePlayersAsync()
        {
            List<FootballPlayer> players = _context.FootballPlayers.Where(_=>_.ClubId==null).ToList();

            return players;
        }
        public async Task CteateAsync(FootballPlayerDTO dto)
        {
            FootballPlayer footballPlayer = new FootballPlayer
            {
                FullName = dto.Name,
                Birth = dto.Birth,
                SNILS = dto.SNILS

            };
            _context.FootballPlayers.Add(footballPlayer);

            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task LeavClub(int id)
        {
            FootballPlayer player = await _context.FootballPlayers.FirstOrDefaultAsync(x => x.Id == id);
            if (player == null)
            {
                throw new Exception("Игрок не найден");
            }

            player.Club = null;

            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<FootballPlayerGetDTO> GetAsync(int id)
        {
            FootballPlayer footballPlayer = await _context.FootballPlayers.FirstOrDefaultAsync(x => x.Id == id);
            if (footballPlayer == null)
            {
                throw new Exception("Игрок не найден");
            }
            FootballClub club = await _context.FootballClubs.FirstOrDefaultAsync(x => x.Id == footballPlayer.ClubId);

            FootballPlayerGetDTO result = new FootballPlayerGetDTO
            {
                Birth = footballPlayer.Birth.ToString(),
                SNILS = footballPlayer.SNILS,
                Name = footballPlayer.FullName,
                Id = footballPlayer.Id

            };

            if (club != null)
            {
                result.Club = club.Name;
                result.ClubId=footballPlayer.ClubId;               

            }

            return result;


        }

        public async Task RemoveAsync(int id)
        {
            FootballPlayer footballPlayer = await _context.FootballPlayers.FirstOrDefaultAsync(x => x.Id == id);
            if (footballPlayer == null)
            {
                throw new Exception("Игрок не найден");
            }
            _context.FootballPlayers.Remove(footballPlayer);

            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(FootballPlayerDTO dto, int id)
        {
            FootballPlayer footballPlayer = await _context.FootballPlayers.FirstOrDefaultAsync(x => x.Id == id);
            if (footballPlayer == null)
            {
                throw new Exception("Игрок не найден");
            }
            footballPlayer.Birth = dto.Birth;
            footballPlayer.SNILS = dto.SNILS;
            footballPlayer.FullName = dto.Name;
        }
    }
}
