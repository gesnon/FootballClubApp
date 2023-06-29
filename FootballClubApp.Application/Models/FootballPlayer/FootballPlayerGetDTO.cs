using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClubApp.Application.Models.FootballPlayer
{
    public class FootballPlayerGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Birth { get; set; }
        public string SNILS { get; set; }
        public string? Club { get; set; }
        public int? ClubId { get; set; }
    }
}
