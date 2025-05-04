using System.Collections;

namespace _Scripts.Model.GamePhase
{
    public interface IGameState
    {
        public void StartingCurrentPhase();
        public void TerminateCurrentPhase();
        public IGameState NextPhase();
    }
}