using System.Collections;
using _Scripts.GameActions;

namespace _Scripts.Model.GamePhase
{
    public class PlayerPhase : GamePhase
    {
        public override void StartingCurrentPhase()
        {
            ActionSystem.Instance.AddReaction(new ResetManaGA());
            ActionSystem.Instance.AddReaction(new RefillGA());
            ActionSystem.Instance.AddReaction(new DrawCardsGA(5));
        }

        public override void TerminateCurrentPhase()
        {
            ActionSystem.Instance.AddReaction(new DiscardAllCardsGA());
        }

        public override IGamePhase NextPhase()
        {
            return new EnemyPhase();
        }
    }
}