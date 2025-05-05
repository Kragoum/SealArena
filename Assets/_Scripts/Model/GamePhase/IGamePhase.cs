using System.Collections;

namespace _Scripts.Model.GamePhase
{
    public interface IGamePhase
    {
        public void StartingCurrentPhase();
        public void TerminateCurrentPhase();
        public IGamePhase NextPhase();
    }
}