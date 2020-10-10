using System.Collections.Generic;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Core
{
    public class GamePositionProvider
    {
        #region Properties

        private static List<FootballPlayer> KickOffLeft { get; } = new List<FootballPlayer>
        {
            new FootballPlayer
            {
                Id = 1,
                HasBall = false,
                Position = new Position
                {
                    X = -6,
                    Y = 0
                }
            },
            new FootballPlayer
            {
                Id = 2,
                HasBall = false,
                Position = new Position
                {
                    X = -4,
                    Y = -1
                }
            },
            new FootballPlayer
            {
                Id = 3,
                HasBall = false,
                Position = new Position
                {
                    X = -4,
                    Y = 1
                }
            },
            new FootballPlayer
            {
                Id = 4,
                HasBall = false,
                Position = new Position
                {
                    X = -2,
                    Y = -2
                }
            },
            new FootballPlayer
            {
                Id = 5,
                HasBall = true,
                Position = new Position
                {
                    X = 0,
                    Y = 0
                }
            },
            new FootballPlayer
            {
                Id = 6,
                HasBall = false,
                Position = new Position
                {
                    X = 0,
                    Y = 1
                }
            },
            new FootballPlayer
            {
                Id = 7,
                HasBall = false,
                Position = new Position
                {
                    X = 6,
                    Y = 0
                }
            },
            new FootballPlayer
            {
                Id = 8,
                HasBall = false,
                Position = new Position
                {
                    X = 4,
                    Y = 1
                }
            },
            new FootballPlayer
            {
                Id = 9,
                HasBall = false,
                Position = new Position
                {
                    X = 4,
                    Y = -1
                }
            },
            new FootballPlayer
            {
                Id = 10,
                HasBall = false,
                Position = new Position
                {
                    X = 2,
                    Y = -2
                }
            },
            new FootballPlayer
            {
                Id = 11,
                HasBall = false,
                Position = new Position
                {
                    X = 2,
                    Y = 0
                }
            },
            new FootballPlayer
            {
                Id = 12,
                HasBall = false,
                Position = new Position
                {
                    X = 2,
                    Y = 2
                }
            }
        };

        private static List<FootballPlayer> KickOffRight { get; } = new List<FootballPlayer>
        {
            new FootballPlayer
            {
                Id = 1,
                HasBall = false,
                Position = new Position
                {
                    X = -6,
                    Y = 0
                }
            },
            new FootballPlayer
            {
                Id = 2,
                HasBall = false,
                Position = new Position
                {
                    X = -4,
                    Y = -1
                }
            },
            new FootballPlayer
            {
                Id = 3,
                HasBall = false,
                Position = new Position
                {
                    X = -4,
                    Y = 1
                }
            },
            new FootballPlayer
            {
                Id = 4,
                HasBall = false,
                Position = new Position
                {
                    X = -2,
                    Y = -2
                }
            },
            new FootballPlayer
            {
                Id = 5,
                HasBall = false,
                Position = new Position
                {
                    X = -2,
                    Y = 0
                }
            },
            new FootballPlayer
            {
                Id = 6,
                HasBall = false,
                Position = new Position
                {
                    X = -2,
                    Y = 2
                }
            },
            new FootballPlayer
            {
                Id = 7,
                HasBall = false,
                Position = new Position
                {
                    X = 6,
                    Y = 0
                }
            },
            new FootballPlayer
            {
                Id = 8,
                HasBall = false,
                Position = new Position
                {
                    X = 4,
                    Y = 1
                }
            },
            new FootballPlayer
            {
                Id = 9,
                HasBall = false,
                Position = new Position
                {
                    X = 4,
                    Y = -1
                }
            },
            new FootballPlayer
            {
                Id = 10,
                HasBall = false,
                Position = new Position
                {
                    X = 0,
                    Y = -1
                }
            },
            new FootballPlayer
            {
                Id = 11,
                HasBall = true,
                Position = new Position
                {
                    X = 0,
                    Y = 0
                }
            },
            new FootballPlayer
            {
                Id = 12,
                HasBall = false,
                Position = new Position
                {
                    X = 2,
                    Y = 2
                }
            }
        };

        public List<FootballPlayer> Provide(TeamType teamType, HalfTime halfTime)
        {
            switch (teamType)
            {
                case TeamType.Home when halfTime == HalfTime.First:
                case TeamType.Away when halfTime == HalfTime.Second:
                    return CreateCopy(KickOffLeft);
                case TeamType.Home when halfTime == HalfTime.Second:
                case TeamType.Away when halfTime == HalfTime.First:
                    return CreateCopy(KickOffRight);
                default: return CreateCopy(KickOffRight);
            }
        }

        private List<FootballPlayer> CreateCopy(List<FootballPlayer> players)
        {
            List<FootballPlayer> footballPlayers = new List<FootballPlayer>();
            foreach (FootballPlayer player in players)
            {
                footballPlayers.Add(player.Clone());
            }
            return footballPlayers;
        }
        
        
        #endregion
    }
}