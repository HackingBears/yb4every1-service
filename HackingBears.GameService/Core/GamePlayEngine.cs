using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Timers;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Core
{
    public sealed class GamePlayEngine : IGamePlayEngine
    {
        #region Events

        public event EventHandler<GameFrameEventArgs> OnFrameChanged;
        public event EventHandler<GameFinishedEventArgs> OnGameFinished;

        #endregion

        #region Properties

        public int GameTime { get; set; }

        public GameStateDescription State { get; } = GameStateDescription.OpenForRegistration;

        private int FrameCounter { get; set; }

        private int GameId { get; } = 1;

        private PlayerIdSelector PlayerIdSelector { get; } = new PlayerIdSelector();

        private Timer Timer { get; }

        #endregion

        #region Constructor

        public GamePlayEngine()
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

        public GameRegistration RegisterPlayer(in string userId, TeamType teamType)
            => new GameRegistration
            {
                PlayerId = PlayerIdSelector.Register(teamType),
                UserId = userId
            };

        public GameFrame GetCurrentFrame()
            => CreateGameFrame();

        private void Timer_OnElapsed(object sender, ElapsedEventArgs e)
        {
            GameFrame frame = CreateGameFrame();
            Console.WriteLine(JsonSerializer.Serialize(frame));

            OnFrameChanged?.Invoke(this, new GameFrameEventArgs
                {
                    Frame = CreateGameFrame(),
                    GameId = GameId
                }
            );
            Console.WriteLine(GameTime);
            if (GameTime >= 90)
            {
                Timer.Stop();
                Console.WriteLine("Game - Stopped");
                OnGameFinished?.Invoke(this, new GameFinishedEventArgs
                    {
                        GameId = GameId
                    }
                );
            }
            else
            {
                GameTime += 3;
                FrameCounter += 1;
            }
        }

        public void Start()
        {
            GameTime = 0;
            Timer.Start();
        }

        public void AddAction(GameAction action)
        {
            //Do Nothing
        }

        private GameFrame CreateGameFrame()
        {
            GameFrame frame = new GameFrame();
            frame.FrameExpiration = 3;
            frame.GameId = GameId;
            frame.FrameNumber = FrameCounter;
            frame.GameEvent = string.Empty;
            frame.GameScore = "0:0";
            frame.GameTime = GameTime.ToString("00") + " min";
            frame.Players = new List<FootballPlayer>
            {
                CreateYbGoalKeeper(),
                CreateYbPlayer(2),
                CreateYbPlayer(3),
                CreateYbPlayer(4),
                CreateYbPlayer(5),
                CreateYbPlayer(6),
                CreateBaselGoalKeeper(),
                CreateBaselPlayer(7),
                CreateBaselPlayer(8),
                CreateBaselPlayer(9),
                CreateBaselPlayer(10),
                CreateBaselPlayer(11)
            };

            return frame;
        }

        private FootballPlayer CreateYbGoalKeeper()
            => new FootballPlayer
            {
                Id = 1,
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
                Position = FootballField.CreateRandomPosition()
            };

        private FootballPlayer CreateBaselGoalKeeper()
            => new FootballPlayer
            {
                Id = 6,
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
                Position = FootballField.CreateRandomPosition()
            };

        #endregion
    }
}