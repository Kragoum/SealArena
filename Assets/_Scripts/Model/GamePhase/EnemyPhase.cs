using System.Collections;
using _Scripts.GameActions;
using _Scripts.Systems;
using UnityEngine;

namespace _Scripts.Model.GamePhase
{
    public class EnemyPhase :  GameState
    {
        public override void StartingCurrentPhase()
        {
            ActionSystem.Instance.AddReaction(new EnemyTurnGA());
            ActionSystem.Instance.AddReaction(new EndPhaseGA());
        }

        public override IGameState NextPhase()
        {
            return new PlayerPhase();
        }
    }
}