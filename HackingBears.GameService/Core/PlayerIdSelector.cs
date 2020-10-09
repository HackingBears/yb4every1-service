using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Core
{
    public sealed class PlayerIdSelector
    {
        #region Properties

        private Dictionary<int, int> AwayTeamPlayers { get; } = new Dictionary<int, int>
        {
            {7, 0},
            {8, 0},
            {9, 0},
            {10, 0},
            {11, 0},
            {12, 0}
        };

        private Dictionary<int, int> HomeTeamPlayers { get; } = new Dictionary<int, int>
        {
            {1, 0},
            {2, 0},
            {3, 0},
            {4, 0},
            {5, 0},
            {6, 0}
        };

        #endregion

        #region Methods

        public int Register(TeamType teamType)
            => GetNextFotballPlayerId(teamType == TeamType.Home ? HomeTeamPlayers : AwayTeamPlayers);

        private int GetNextFotballPlayerId(Dictionary<int, int> players)
        {
            lock (((ICollection) players).SyncRoot)
            {
                int minValue = players.Values.Min();
                int key = players.First(kv => kv.Value == minValue).Key;
                players[key] = minValue + 1;
                return key;
            }
        }

        #endregion
    }
}