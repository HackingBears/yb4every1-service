using System.Threading.Tasks;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Hubs
{
    public interface IGameHubAction
    {
        #region Methods

        /// <summary>
        ///     Aktualisieren eines Spiel-Frames
        /// </summary>
        /// <param name="frame">Aktuelle Spielinformationen</param>
        Task UpdateGameFrame(GameFrame frame);

        /// <summary>
        ///     Abschliessen der Registrierung
        /// </summary>
        /// <param name="gameRegistration"></param>
        Task CompleteRegistration(GameRegistration gameRegistration);

        /// <summary>
        ///     Spiel beenden
        /// </summary>
        Task GameFinished();

        /// <summary>
        ///     Tor
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        Task Goal(GameFrame frame);

        /// <summary>
        ///     Event - welches während des Spiels passieren kann (zum Beispiel Gelbe Kart, Rote Karte, Halbzeit)
        /// </summary>
        Task GameEventHappened(GameEvent gameEvent);

        #endregion
    }
}