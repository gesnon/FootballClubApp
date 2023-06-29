
using FootballClubApp.Application.Interfaces;
using FootballClubApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Infrastructure.Persistence
{
    public class FootballClubAppContext : DbContext, IFootballClubAppContext
    {

        public FootballClubAppContext(DbContextOptions<FootballClubAppContext> options) : base(options)
        {
        }

        public DbSet<FanClub> FanClubs { get; set; }
        public DbSet<FootballClub> FootballClubs { get; set; }
        public DbSet<FootballFan> FootballFans { get; set; }
        public DbSet<FootballPlayer> FootballPlayers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FootballClubAppContext).Assembly);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.SetNull;

        }
    }
}
