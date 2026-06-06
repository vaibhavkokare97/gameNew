using Assets.Scripts.Controller;
using System;

namespace Assets.Scripts.UI
{
    public class UIManager
    {
        /// <summary>
        /// Event triggered when the Start Game button is clicked, passing the selected difficulty and seed.
        /// </summary>
        public static Action<(int, int), int, int> OnGameStartUI;

        /// <summary>
        /// Event triggered when the Stop Game button is clicked, signaling to stop the game and return to the main menu. bool denotes whether the game was won or not, allowing for different handling if needed.
        /// </summary>
        public static Action<bool?> OnGameStopUI;

        public static Action<int[], int, int> OnLoadState;
    }
}
