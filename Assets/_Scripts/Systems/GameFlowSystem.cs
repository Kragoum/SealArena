using System.Collections;
using _Scripts.GameActions;
using _Scripts.Model.GamePhase;
using UnityEngine;

namespace _Scripts.Systems
{
    
    
    public class GameFlowSystem : Singleton<GameFlowSystem>
    {
        private IGameState _gameState = new PlayerPhase();
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
            Debug.Log($"End current phase : {_gameState.GetType()}");
            _gameState.TerminateCurrentPhase();
            _gameState = _gameState.NextPhase();
            _gameState.StartingCurrentPhase();
            Debug.Log($"New current phase : {_gameState.GetType()}");
            yield return null;
        }
    }
}