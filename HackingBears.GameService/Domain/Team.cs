using System.Collections.Generic;

namespace HackingBears.GameService.Domain
{
    public class Team
    {
        public string ClubName { get; set; }
        
        public TeamType Type { get; set; }
        
        public List<FootballPlayer> Players { get; set; }
        
    }
}