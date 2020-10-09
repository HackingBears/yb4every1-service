using System;

namespace HackingBears.GameService.Domain
{
    public class GameRegistration
    {
        #region Properties

        public Guid GameId { get; set; }
        public long PlayerId { get; set; }
        public long UserId { get; set; }

        #endregion
    }
}