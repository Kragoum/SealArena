using System.Collections;
using _Scripts.GameActions;

namespace _Scripts.Model.GamePhase
{
    public class PlayerPhase : IGameState
    {
        public IEnumerator StartingCurrentPhase()
        {
            ActionSystem.Instance.AddReaction(new RefillGA());
            ActionSystem.Instance.AddReaction(new DrawCardsGA(5));
            yield return null;
        }

        public IEnumerator TerminateCurrentPhase()
        {
            ActionSystem.Instance.AddReaction(new DiscardAllCardsGA());
            yield return null;
        }

        public IGameState NextPhase()
        {
            return new EnemyPhase();
        }
    }
}