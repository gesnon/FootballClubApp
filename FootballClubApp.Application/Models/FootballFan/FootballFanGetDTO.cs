using FootballClubApp.Application.Models.FootballClub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Models.FootballFan
{
    public class FootballFanGetDTO
    {
        public string Name  { get; set; }
        public int Id { get; set; }
        public List<FootballClubPreviewDTO> FollowedClubDTOs { get; set; }
    }
}
