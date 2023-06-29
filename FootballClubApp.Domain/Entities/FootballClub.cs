using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Domain.Entities
{
    public class FootballClub : Entity
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
}
