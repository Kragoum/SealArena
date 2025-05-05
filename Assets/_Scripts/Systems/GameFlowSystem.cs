using System.Collections;
using _Scripts.GameActions;
using _Scripts.Model.GamePhase;
using UnityEngine;

namespace _Scripts.Systems
{
    
    
    public class GameFlowSystem : Singleton<GameFlowSystem>
    {
        private IGamePhase _gamePhase = new PlayerPhase();
        private void OnEnable()
        {
            ActionSystem.AttachPerformer<EndPhaseGA>(EndPhasePerformer);
        }
        
        private void OnDisable()
        {
            ActionSystem.DetachPerformer<EndPhaseGA>();
        }
        
        private IEnumerator EndPhasePerformer(EndPhaseGA action)
        {
            Debug.Log($"End current phase : {_gamePhase.GetType()}");
            _gamePhase.TerminateCurrentPhase();
            _gamePhase = _gamePhase.NextPhase();
            _gamePhase.StartingCurrentPhase();
            Debug.Log($"New current phase : {_gamePhase.GetType()}");
            yield return null;
        }
    }
}