using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Timers;
using HackingBears.GameService.Domain;
using Action = HackingBears.GameService.Domain.Action;

namespace HackingBears.GameService.Core
{
    public sealed class GamePlayEngine : IGamePlayEngine
    {
        #region Constants

        private const int SHOOT_FACTOR = 3;

        private const int VOTING_TIME_IN_SECONDS = 3;

        #endregion

        #region Events

        public event EventHandler<GameFrameEventArgs> OnFrameChanged;
        public event EventHandler<GameFinishedEventArgs> OnGameFinished;
        public event EventHandler<GoalEventArgs> OnGoal;

        #endregion

        #region Properties

        public GameFrame CurrentFrame { get; private set; }

        public int GameTime { get; set; }

        public GameStateDescription State { get; } = GameStateDescription.OpenForRegistration;

        private int FrameCounter { get; set; }

        private int GameId { get; }

        private GamePositionProvider GamePositionProvider { get; } = new GamePositionProvider();

        private PlayerIdSelector PlayerIdSelector { get; } = new PlayerIdSelector();

        private Score Score { get; } = new Score();

        private Timer Timer { get; }

        private VotingManager VotingManager { get; }

        #endregion

        #region Constructor

        public GamePlayEngine(int gameId)
        {
            Timer = new Timer
            {
                Interval = VOTING_TIME_IN_SECONDS * 1000,
                AutoReset = true
            };
            Timer.Elapsed += Timer_OnElapsed;
            VotingManager = new VotingManager(12);
            GameId = gameId;
            Init();
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
            => CurrentFrame;

        private void Timer_OnElapsed(object sender, ElapsedEventArgs e)
        {
            // Frame klonen
            GameFrame frame = GetNextFrame(VotingManager.GetResult(FrameCounter + 1));

            Console.WriteLine(JsonSerializer.Serialize(frame));

            OnFrameChanged?.Invoke(this, new GameFrameEventArgs
                {
                    Frame = frame,
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
                GameTime += VOTING_TIME_IN_SECONDS;
                FrameCounter += 1;
            }
        }

        public void Start()
        {
            Init();
            Timer.Start();
        }

        private void Init()
        {
            GameTime = 0;
            Score.Reset();
            FrameCounter = 0;
            CurrentFrame = GetStartFrame();
        }

        public void AddVoting(Voting voting)
        {
            // Voting nur annehmen wenn FrameNumber dem naechsten Frame entspricht
            if (voting.FrameNumber == (FrameCounter + 1))
            {
                VotingManager.AddVoting(voting);
            }
        }

        public GameFrame GetStartFrame()
            => new GameFrame
            {
                Ball = new Position(),
                FrameNumber = 0,
                GameEvent = "Anstoss 1. Halbzeit",
                GameId = GameId,
                Players = GamePositionProvider.Provide(TeamType.Home, HalfTime.First),
                GameScore = Score.ToString(),
                GameTime = "00 min",
                FrameExpiration = VOTING_TIME_IN_SECONDS
            };

        public GameFrame GetNextFrame(List<VotingResult> results)
        {
            GameFrame newFrame = CurrentFrame.Clone();
            newFrame.FrameNumber = FrameCounter + 1;

            // Limiten Prüfung
            newFrame.Players.ForEach(p => ApplyMotions(p, results));

            // Limiten Prüfung Ball
            ApplyPositionLimits(newFrame.Ball);

            // Has Ball 
            newFrame.Players.ForEach(p => p.HasBall = HasBall(p.Position, newFrame.Ball));

            return newFrame;
        }

        private void ApplyMotions(FootballPlayer player, List<VotingResult> results)
        {
            VotingResult result = results.FirstOrDefault(vr => vr.PlayerId == player.Id);
            if (result.GameAction.Action == Action.Run)
            {
                player.Position = player.Position + result.GameAction.Direction.ToPosition();
            }

            ApplyPositionLimits(player.Position);
        }

        private bool IsHomeGoal(Position oldBallPosition, Position newBallPosition)
        {
            if (newBallPosition.X <= FootballField.MAX_X)
            {
                return false;
            }

            if (newBallPosition.X == oldBallPosition.X)
            {
                return false;
            }

            return false;
        }

        private bool IsAwayGoal(Position oldBallPosition, Position newBallPosition)
        {
            if (newBallPosition.X >= FootballField.MIN_Y)
            {
                return false;
            }

            if (newBallPosition.X == oldBallPosition.X)
            {
                return false;
            }

            double steigung = (newBallPosition.Y - oldBallPosition.Y) / (newBallPosition.X - oldBallPosition.X);
            double offset = newBallPosition.Y - (steigung * newBallPosition.X);

            return false;
        }

        private bool HasBall(Position player, Position ball)
            => player == ball;

        private void ApplyPositionLimits(Position position)
        {
            if (position.X < FootballField.MIN_X)
            {
                position.X = FootballField.MIN_X;
            }

            if (position.X > FootballField.MAX_X)
            {
                position.X = FootballField.MAX_Y;
            }

            if (position.Y < FootballField.MIN_Y)
            {
                position.Y = FootballField.MIN_Y;
            }

            if (position.Y > FootballField.MAX_Y)
            {
                position.Y = FootballField.MAX_Y;
            }
        }

        public void GameFinished()
        { 
            
        }

        #endregion
    }
}