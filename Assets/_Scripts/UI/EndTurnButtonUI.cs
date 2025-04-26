using UnityEngine;

public class EndTurnUIButton : MonoBehaviour
{
    public void OnClick()
    {
        EnemyTurnGA enemyTurnGA = new();
        ActionSystem.Instance.Perform(enemyTurnGA);
    }
}
