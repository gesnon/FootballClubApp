using FootballClubApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Interfaces
{
    public interface IFootballClubAppContext
    {
        public DbSet<FanClub> FanClubs { get; set; }
        public DbSet<FootballClub> FootballClubs { get; set; }
        public DbSet<FootballFan> FootballFans { get; set; }
        public DbSet<FootballPlayer> FootballPlayers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
