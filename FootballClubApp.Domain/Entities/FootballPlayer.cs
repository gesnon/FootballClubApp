using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Domain.Entities
{
    public class FootballPlayer : Entity
    {
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string SNILS { get; set; }
        public int? ClubId { get; set; }
        public FootballClub? Club { get; set; }

    }
}
