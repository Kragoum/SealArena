using System.Collections;
using _Scripts.Systems;
using UnityEngine;

namespace _Scripts.Model.GamePhase
{
    public class EnemyPhase :  IGameState
    {
        public IEnumerator StartingCurrentPhase()
        {
            Debug.Log("Enemy turn...");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Enemy is thinking hard...");
            yield return new WaitForSeconds(2f);
            Debug.Log("Enemy postpone it's terrible scheme!");
            ActionSystem.Instance.AddReaction(new EndPhase());
        }

        public IEnumerator TerminateCurrentPhase()
        {
            yield return null;
        }

        public IGameState NextPhase()
        {
            return new PlayerPhase();
        }
    }
}