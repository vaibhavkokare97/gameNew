using Assets.Scripts.Controller;
using System;

namespace Assets.Scripts.UI
{
    public class UIManager
    {
        /// <summary>
        /// Event triggered when the Start Game button is clicked, passing the selected difficulty and seed.
        /// </summary>
        public static Action<(int, int), int> OnGameStartUI;
        public static Action OnGameStopUI;
    }
}
