namespace Assets.Scripts.State
{
    public static class AppState
    {
        public enum State
        {
            MainMenu,
            InGame
        }

        public static State currentState = AppState.State.MainMenu;
    }
}