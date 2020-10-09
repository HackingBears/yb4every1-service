using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Timers;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Core
{
    public sealed class GameEngine : IGameEngine
    {
        #region Events

        public event EventHandler<GameFrameEventArgs> OnFrameChanged;

        #endregion

        #region Properties

        public int GameTime { get; set; }

        public GameState State { get; } = GameState.NotStarted;

        private int FrameCounter { get; set; }

        private Guid GameId { get; } = Guid.NewGuid();

        private Timer Timer { get; }

        #endregion

        #region Constructor

        public GameEngine()
        {
            Timer = new Timer
            {
                Interval = 3000,
                AutoReset = true
            };
            Timer.Elapsed += Timer_OnElapsed;
        }

        #endregion

        #region Methods

        private void Timer_OnElapsed(object sender, ElapsedEventArgs e)
        {
            GameFrame frame = CreateGameFrame();
            Console.WriteLine(JsonSerializer.Serialize(frame));

            OnFrameChanged?.Invoke(this, new GameFrameEventArgs
                {
                    Frame = CreateGameFrame()
                }
            );
            Console.WriteLine(GameTime);
            if (GameTime >= 90)
            {
                Timer.Stop();
                Console.WriteLine("Game - Stopped");
            }
            else
            {
                GameTime += 3;
                FrameCounter += 1;
            }
        }

        public GameFrame GetCurrentFrame(Guid registrationGameId)
            => CreateGameFrame();

        public void Start()
        {
            GameTime = 0;
            Timer.Start();
        }

        public void AddAction(GameAction action)
        {
            //Do Nothing
        }

        public GameRegistration RegisterPlayer(in long userId)
            => new GameRegistration
            {
                GameId = GameId,
                PlayerId = 5,
                UserId = userId
            };

        private GameFrame CreateGameFrame()
        {
            GameFrame frame = new GameFrame();
            frame.FrameExpiration = 3;
            frame.GameId = GameId;
            frame.FrameNumber = FrameCounter;
            frame.GameEvent = string.Empty;
            frame.GameScore = "0:0";
            frame.GameTime = GameTime.ToString("00") + " min";
            frame.Teams = new List<Team>
            {
                new Team
                {
                    ClubName = "YB",
                    Type = TeamType.Home,
                    Players = new List<FootballPlayer>
                    {
                        CreateYbGoalKeeper(),
                        CreateYbPlayer(2),
                        CreateYbPlayer(3),
                        CreateYbPlayer(4),
                        CreateYbPlayer(5),
                        CreateYbPlayer(6)
                    }
                },
                new Team
                {
                    ClubName = "FC Basel",
                    Type = TeamType.Away,
                    Players = new List<FootballPlayer>
                    {
                        CreateBaselGoalKeeper(),
                        CreateBaselPlayer(7),
                        CreateBaselPlayer(8),
                        CreateBaselPlayer(9),
                        CreateBaselPlayer(10),
                        CreateBaselPlayer(11)
                    }
                }
            };

            return frame;
        }

        private FootballPlayer CreateYbGoalKeeper()
            => new FootballPlayer
            {
                Id = 1,
                JerseyColor = "#000000",
                ShortColor = "#F7FE2E",
                JerseyNumber = 1,
                Position = new Position
                {
                    X = 0,
                    Y = -5
                }
            };

        private FootballPlayer CreateYbPlayer(int id)
            => new FootballPlayer
            {
                Id = id,
                JerseyColor = "#F7FE2E",
                ShortColor = "#000000",
                JerseyNumber = id,
                Position = FootballField.CreateRandomPosition()
            };

        private FootballPlayer CreateBaselGoalKeeper()
            => new FootballPlayer
            {
                Id = 6,
                JerseyColor = "##0404B4",
                ShortColor = "##FF0000",
                JerseyNumber = 1,
                Position = new Position
                {
                    X = 0,
                    Y = 5
                }
            };

        private FootballPlayer CreateBaselPlayer(int id)
            => new FootballPlayer
            {
                Id = id,
                JerseyColor = "#FF0000",
                ShortColor = "##0404B4",
                JerseyNumber = id - 5,
                Position = FootballField.CreateRandomPosition()
            };

        #endregion
    }
}