using System.Collections;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
    }

    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        Debug.Log("Enemy turn");
        yield return new WaitForSeconds(1f);
        Debug.Log("Enemy end his turn");
    }
}
