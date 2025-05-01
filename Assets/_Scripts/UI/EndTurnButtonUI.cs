using _Scripts.Systems;
using UnityEngine;

public class EndTurnUIButton : MonoBehaviour
{
    public void OnClick()
    {
        var endPlayerPhase = new EndPhase();
        ActionSystem.Instance.Perform(endPlayerPhase);
    }
}
