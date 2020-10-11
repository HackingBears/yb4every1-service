using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Timers;
using HackingBears.GameService.Domain;
using Action = HackingBears.GameService.Domain.Action;
using Timer = System.Timers.Timer;

namespace HackingBears.GameService.Core
{
    public sealed class GamePlayEngine : IGamePlayEngine, IDisposable
    {
        #region Constants

        private const int SHOOT_FACTOR = 2;

        private const int VOTING_TIME_IN_SECONDS = 6;

        #endregion

        #region Events

        public event EventHandler<GameFrameEventArgs> OnFrameChanged;
        public event EventHandler<GameEventEventArgs> OnGameEventHappened;
        public event EventHandler<GameFinishedEventArgs> OnGameFinished;
        public event EventHandler<GameFrameEventArgs> OnGoal;

        #endregion

        #region Properties

        public GameFrame CurrentFrame { get; private set; }

        public int GameTime
            => (FrameCounter * VOTING_TIME_IN_SECONDS) / 6;

        public GameStateDescription State { get; } = GameStateDescription.OpenForRegistration;

        private Timer CountDownTimer { get; }

        private int FrameCounter { get; set; }

        private int GameId { get; }

        private GamePositionProvider GamePositionProvider { get; } = new GamePositionProvider();

        private PlayerIdSelector PlayerIdSelector { get; } = new PlayerIdSelector();

        private Random Random { get; } = new Random();

        private Score Score { get; } = new Score();

        private int SecondsToGameStart { get; set; } = 15;

        private Timer Timer { get; }

        private VotingManager VotingManager { get; }

        #endregion

        #region Constructor

        public GamePlayEngine(int gameId)
        {
            Console.WriteLine($"Game registered - will start in {SecondsToGameStart} seconds");
            Timer = new Timer
            {
                Interval = VOTING_TIME_IN_SECONDS * 1000,
                AutoReset = false
            };
            Timer.Elapsed += Timer_OnElapsed;
            VotingManager = new VotingManager(12);
            GameId = gameId;

            Init();
            CountDownTimer = new Timer
            {
                Interval = 1000,
                AutoReset = true
            };
            CountDownTimer.Elapsed += CountDown_OnSecondElapsed;
            CountDownTimer.Start();
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
        {
            GameFrame frame = CurrentFrame;
            frame.GameTime = GameTime.ToString("00") + " min";
            return frame;
        }

        private void Timer_OnElapsed(object sender, ElapsedEventArgs e)
        {
            // Frame klonen
            GameFrame frame = GetNextFrame(VotingManager.GetResult(FrameCounter + 1));

            if (IsHomeGoal(frame.Ball))
            {
                Score.AddHome();
                PublishGoalNotification(frame);
                FrameCounter++;
                Thread.Sleep(VOTING_TIME_IN_SECONDS * 1000);
                frame = GetKickOffFrame(TeamType.Away);
            }

            if (IsAwayGoal(frame.Ball))
            {
                Score.AddAway();
                PublishGoalNotification(frame);
                FrameCounter++;
                Thread.Sleep(VOTING_TIME_IN_SECONDS * 1000);
                frame = GetKickOffFrame(TeamType.Home);
            }

            PublishFrameChange(frame);

            Console.WriteLine(GameTime);
            if (GameTime >= 90)
            {
                Timer.Stop();
                Console.WriteLine("Game - Stopped");
                PublishGameFinished();
            }
            else
            {
                Timer.Start();
            }

            CurrentFrame = frame;
        }

        private void PublishGameFinished()
        {
            OnGameFinished?.Invoke(this, new GameFinishedEventArgs
                {
                    GameId = GameId
                }
            );
        }

        private void PublishFrameChange(GameFrame frame)
        {
            frame.GameTime = GameTime.ToString("00") + " min";
            frame.GameScore = Score.ToString();
            OnFrameChanged?.Invoke(this, new GameFrameEventArgs
                {
                    Frame = frame,
                    GameId = GameId
                }
            );
        }

        private void PublishGoalNotification(GameFrame frame)
        {
            frame.GameTime = GameTime.ToString("00") + " min";
            frame.GameScore = Score.ToString();
            frame.GameEvent = "Gooooooal!";

            OnGoal?.Invoke(this, new GameFrameEventArgs
                {
                    Frame = frame,
                    GameId = GameId
                }
            );
            Console.WriteLine(JsonSerializer.Serialize(frame));
        }

        public void Start()
        {
            CountDownTimer.AutoReset = false;
            CountDownTimer.Stop();

            Init();
            Console.WriteLine("Game is started");
            Timer.Start();
        }

        private void Init()
        {
            Score.Reset();
            FrameCounter = 0;
            VotingManager.Reset();
            CurrentFrame = GetKickOffFrame(TeamType.Home);
            SecondsToGameStart = 15;
        }

        public void AddVoting(Voting voting)
        {
            // Voting nur annehmen wenn FrameNumber dem naechsten Frame entspricht
            if (voting.FrameNumber == (FrameCounter + 1))
            {
                VotingManager.AddVoting(voting);
            }
        }

        public GameFrame GetKickOffFrame(TeamType teamType)
            => new GameFrame
            {
                Ball = new Position(),
                FrameNumber = FrameCounter,
                GameEvent = "",
                GameId = GameId,
                Players = GamePositionProvider.Provide(teamType, HalfTime.First),
                GameScore = Score.ToString(),
                FrameExpiration = VOTING_TIME_IN_SECONDS
            };

        public GameFrame GetNextFrame(List<VotingResult> results)
        {
            FrameCounter++;
            GameFrame newFrame = CurrentFrame.Clone();
            newFrame.GameEvent = string.Empty;
            newFrame.FrameNumber = FrameCounter;

            // Spieler Bewegungen hinzufügen
            newFrame.Players.ForEach(p => ApplyPlayerMotions(p, results));

            // Ball bewegungen
            ApplyBallPosition(newFrame, results.FirstOrDefault(r => r.GameAction.Action == Action.Shoot));

            // Has Ball 
            AttributeBallToPlayer(newFrame.Players, newFrame.Ball);

            return newFrame;
        }

        private void AttributeBallToPlayer(List<FootballPlayer> players, Position ball)
        {
            players.ForEach(p => p.HasBall = HasBall(p.Position, ball));
            List<FootballPlayer> playersWithBall = players.Where(fp => fp.HasBall).ToList();
            int playersWithBallCount = playersWithBall.Count;
            if (playersWithBallCount <= 1)
            {
                return;
            }

            int playerId = playersWithBall[Random.Next(0, playersWithBallCount)].Id;
            players.ForEach(p => p.HasBall = p.Id == playerId);
        }

        private void ApplyBallPosition(GameFrame frame, VotingResult result)
        {
            if (result == null)
            {
                return;
            }

            for (int cnt = 0; cnt < SHOOT_FACTOR; cnt++)
            {
                frame.Ball = frame.Ball + result.GameAction.Direction.ToPosition();
                if (HasFieldLimitsAchieved(frame.Ball))
                {
                    break;
                }
            }

            ApplyPositionLimits(frame.Ball);
        }

        private void ApplyPlayerMotions(FootballPlayer player, List<VotingResult> results)
        {
            VotingResult result = results.FirstOrDefault(vr => vr.PlayerId == player.Id);

            if (result == null)
            {
                return;
            }

            if (result.GameAction.Action == Action.Run)
            {
                player.Position = player.Position + result.GameAction.Direction.ToPosition();
            }

            ApplyPositionLimits(player.Position);
        }

        private bool IsHomeGoal(Position ballPosition)
            => (ballPosition.X == FootballField.MAX_X) && (FootballField.TOR_MIN_Y <= ballPosition.Y) && (ballPosition.Y <= FootballField.TOR_MAX_Y);

        private bool IsAwayGoal(Position ballPosition)
            => (ballPosition.X == FootballField.MIN_X) && (FootballField.TOR_MIN_Y <= ballPosition.Y) && (ballPosition.Y <= FootballField.TOR_MAX_Y);

        private bool HasBall(Position player, Position ball)
            => Equals(player.X, ball.X) && Equals(player.Y, ball.Y);

        private bool HasFieldLimitsAchieved(Position position)
            => (position.X == FootballField.MIN_X) || (position.X == FootballField.MAX_X) || (position.Y == FootballField.MIN_Y) || (position.Y == FootballField.MAX_Y);

        private void ApplyPositionLimits(Position position)
        {
            if (position.X < FootballField.MIN_X)
            {
                position.X = FootballField.MIN_X;
            }

            if (position.X > FootballField.MAX_X)
            {
                position.X = FootballField.MAX_X;
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

        private void CountDown_OnSecondElapsed(object sender, ElapsedEventArgs e)
        {
            if (SecondsToGameStart <= 0)
            {
                Start();
                return;
            }

            PublishGameEvent(new GameEvent(GameId, $"Spiel beginnt in {SecondsToGameStart} Sekunden"));
            SecondsToGameStart--;
        }

        private void PublishGameEvent(GameEvent gameEvent)
        {
            OnGameEventHappened?.Invoke(this, new GameEventEventArgs(gameEvent));
        }

        public void Dispose()
        {
            Timer.Stop();
            Timer.Elapsed -= Timer_OnElapsed;
            Timer?.Dispose();

            CountDownTimer.Stop();
            CountDownTimer.Elapsed -= CountDown_OnSecondElapsed;
            CountDownTimer?.Dispose();
        }

        #endregion
    }
}