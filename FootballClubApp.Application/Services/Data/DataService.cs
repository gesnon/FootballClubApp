using FootballClubApp.Application.Interfaces;
using FootballClubApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Services.Data
{
    public class DataService : IDataService
    {
        private readonly IFootballClubAppContext _context;

        public DataService(IFootballClubAppContext context)
        {
            _context = context;
        }

        public async Task FillData()
        {
            // Добавление фанатов
            for (int i = 1; i < 10; i++)
            {
                _context.FootballFans.Add(new FootballFan
                {
                    FullName = $"FootballFanName {i}",
                    FollowClubs = new List<FanClub>()
                });
                await _context.SaveChangesAsync(CancellationToken.None);
            }

            // Добавление клубов
            for (int i = 1; i < 11; i++)
            {
                _context.FootballClubs.Add(new FootballClub
                {
                    Name = $"FootballClubName {i}",
                    City = $"CityName {i}"

                });
                await _context.SaveChangesAsync(CancellationToken.None);
            }

            //Добавление игроков            

            List<int> clubIds = _context.FootballClubs.Select(x => x.Id).ToList();

            for(int p = 0; p < clubIds.Count; p++)
            {
                for (int i = 1; i < 21; i++)
                {
                    if (p == clubIds.Count-1)
                    {
                        _context.FootballPlayers.Add(new FootballPlayer
                        {
                            Birth = DateTime.Now,
                            FullName = $" Team{clubIds[p]} PlayerName {i}",
                            SNILS = $"111111111111"                            
                        });
                        continue;
                    }
                    _context.FootballPlayers.Add(new FootballPlayer
                    {
                        Birth = DateTime.Now,
                        FullName = $" Team{clubIds[p]} PlayerName {i}",
                        SNILS = $"111111111111",
                        ClubId = clubIds[p]
                    });


                    await _context.SaveChangesAsync(CancellationToken.None);
                }
            }

            //foreach (var clubId in clubIds)
            //{
            //    for (int i = 1; i < 21; i++)
            //    {
            //        _context.FootballPlayers.Add(new FootballPlayer
            //        {
            //            Birth = DateTime.Now,
            //            FullName= $" Team{clubId} PlayerName {i}",
            //            SNILS= $"111111111111",
            //            ClubId=clubId                        
            //        });
            //        await _context.SaveChangesAsync(CancellationToken.None);
            //    }
            //}

           

            //Добавление отслеживаемых клубов
            List<int> fanIds = _context.FootballFans.Select(x => x.Id).ToList();

            foreach(var fanId in fanIds)
            {
                for(int i = 0; i < 3; i++)
                _context.FanClubs.Add(new FanClub { FanId=fanId, ClubId=clubIds[i] });
                await _context.SaveChangesAsync(CancellationToken.None);
            }

            List<int> fanIds2 = _context.FootballFans.Select(x => x.Id).ToList();
            fanIds2.Remove(fanIds2[0]);
            fanIds2.Remove(fanIds2[0]);

            foreach (var fanId in fanIds2)
            {
                for (int i = 4; i < 6; i++)
                    _context.FanClubs.Add(new FanClub { FanId = fanId, ClubId = clubIds[i] });
                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }




        public async Task RemoveData()
        {
            _context.FanClubs.RemoveRange(_context.FanClubs);
            _context.FootballClubs.RemoveRange(_context.FootballClubs);
            _context.FootballFans.RemoveRange(_context.FootballFans);
            _context.FootballPlayers.RemoveRange(_context.FootballPlayers);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
