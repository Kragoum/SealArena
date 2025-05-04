using _Scripts.GameActions;
using _Scripts.Systems;
using UnityEngine;

public class EndTurnUIButton : MonoBehaviour
{
    public void OnClick()
    {
        var endPlayerPhase = new EndPhaseGA();
        ActionSystem.Instance.Perform(endPlayerPhase);
    }
}
