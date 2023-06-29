using FootballClubApp.Application.Models.FootballPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Models.FootballClub
{
    public class FootballClubGetDTO
    {
        public string Name { get; set; }
        public string City { get; set; }
        public List<FootballPlayerGetDTO> FootballPlayers { get; set; }
        public int Id { get; set; }
    }
}
