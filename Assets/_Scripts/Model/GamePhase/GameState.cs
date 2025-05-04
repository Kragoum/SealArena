namespace _Scripts.Model.GamePhase
{
    public abstract class GameState : IGameState
    {
        public virtual void StartingCurrentPhase()
        {
        }

        public virtual void TerminateCurrentPhase()
        {
        }

        public abstract IGameState NextPhase();
    }
}