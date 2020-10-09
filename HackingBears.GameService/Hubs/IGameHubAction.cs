using System.Threading.Tasks;
using HackingBears.GameService.Domain;

namespace HackingBears.GameService.Hubs
{
    public interface IGameAction
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

        #endregion
    }
}