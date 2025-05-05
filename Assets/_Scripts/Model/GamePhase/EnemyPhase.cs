using System.Collections;
using _Scripts.GameActions;
using _Scripts.Systems;
using UnityEngine;

namespace _Scripts.Model.GamePhase
{
    public class EnemyPhase :  GamePhase
    {
        public override void StartingCurrentPhase()
        {
            ActionSystem.Instance.AddReaction(new EnemyTurnGA());
            ActionSystem.Instance.AddReaction(new EndPhaseGA());
        }

        public override IGamePhase NextPhase()
        {
            return new PlayerPhase();
        }
    }
}