using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Infrastructure.Persistence
{
    internal class DbInitializer
    {
        private readonly FootballClubAppContext _context;

        public DbInitializer(FootballClubAppContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
