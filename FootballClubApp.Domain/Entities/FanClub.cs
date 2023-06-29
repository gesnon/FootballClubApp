using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Domain.Entities
{
    public class FanClub : Entity
    {
        public int? FanId { get; set; }
        public FootballFan? Fan { get; set; }
        public int? ClubId { get; set; }
        public FootballClub? Club { get; set; }

    }
}
