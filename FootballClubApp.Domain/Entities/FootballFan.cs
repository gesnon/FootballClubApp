using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Domain.Entities
{
    public class FootballFan : Entity
    {
        public string FullName { get; set; }
        public List<FanClub> FollowClubs { get; set; }
    }
}
