using System.Collections;

namespace _Scripts.Model.GamePhase
{
    public interface IGameState
    {
        public IEnumerator StartingCurrentPhase();
        public IEnumerator TerminateCurrentPhase();
        public IGameState NextPhase();
    }
}