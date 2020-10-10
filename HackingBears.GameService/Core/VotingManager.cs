﻿using HackingBears.GameService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackingBears.GameService.Core
{
    public class VotingManager
    {

        #region Properties

        private int PlayerCount { get; }
        private List<Voting> Votings { get; } = new List<Voting>();

        #endregion

        #region Constructor

        public VotingManager(int playerCount)
        {
            PlayerCount = playerCount;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Voting hinzufügen
        /// </summary>
        /// <param name="voting"></param>
        public void AddVoting(Voting voting)
        {
            Voting userVoting = Votings.Where(v => v.UserId == voting.UserId).FirstOrDefault();

            if (userVoting != null)
            {
                // Bestehdendes Voting aktualisieren
                userVoting = voting;
            }
            else
            {
                Votings.Add(voting);
            }
        }

        /// <summary>
        /// Rückgabe der Abstimmungsergebnisse
        /// </summary>
        /// <param name="frameNumber"></param>
        /// <returns></returns>
        public List<VotingResult> GetResult(int frameNumber)
        {
            List<VotingResult> results = new List<VotingResult>();
            IEnumerable<Voting> frameVotings = Votings.Where(v => v.FrameNumber == frameNumber);

            // Resultat zusammenzählen
            for (int i = 0; i < PlayerCount; i++)
            {
                VotingResult result = new VotingResult()
                {
                    PlayerId = i
                };

                result.GameAction = new GameAction
                {
                    Direction = Votings.Where(v => v.PlayerId == i)?.GroupBy(v => v.GameAction?.Direction)
                                        .OrderByDescending(gp => gp.Count()).Select(v => v.Key).FirstOrDefault() ?? Direction.Undefined,
                    Action = Votings.Where(v => v.PlayerId == i)?.GroupBy(v => v.GameAction?.Action)
                                     .OrderByDescending(gp => gp.Count()).Select(v => v.Key).FirstOrDefault() ?? Domain.Action.Undefined
                };


                results.Add(result);
            }

            return results;
        }

        #endregion

    }
} 