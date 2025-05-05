namespace _Scripts.Model.GamePhase
{
    public abstract class GamePhase : IGamePhase
    {
        public virtual void StartingCurrentPhase()
        {
        }

        public virtual void TerminateCurrentPhase()
        {
        }

        public abstract IGamePhase NextPhase();
    }
}