using System.Collections;
using _Scripts.Model.GamePhase;
using UnityEngine;

namespace _Scripts.Systems
{
    public class EndPhase : GameAction
    {
    }
    
    public class GameFlowSystem : Singleton<GameFlowSystem>
    {
        private IGameState _gameState = new PlayerPhase();
        private void OnEnable()
        {
            ActionSystem.AttachPerformer<EndPhase>(EndPhasePerformer);
        }
        
        private void OnDisable()
        {
            ActionSystem.DetachPerformer<EndPhase>();
        }
        
        private IEnumerator EndPhasePerformer(EndPhase action)
        {
            Debug.Log($"End current phase : {_gameState.GetType()}");
            yield return _gameState.TerminateCurrentPhase();
            _gameState = _gameState.NextPhase();
            yield return _gameState.StartingCurrentPhase();
            Debug.Log($"New current phase : {_gameState.GetType()}");
        }
    }
}