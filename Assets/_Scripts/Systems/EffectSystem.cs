using System;
using System.Collections;
using _Scripts.GameActions;
using UnityEngine;

namespace _Scripts.Systems
{
    public class EffectSystem : MonoBehaviour
    {
        private void OnEnable()
        {
            ActionSystem.AttachPerformer<PerformEffectGA>(PerformEffectPerformer);
        }

        private void OnDisable()
        {
            ActionSystem.DetachPerformer<PerformEffectGA>();
        }

        private IEnumerator PerformEffectPerformer(PerformEffectGA effect)
        {
            foreach (var effectAction in effect.Effect.GameActionsEffect())
            {
                ActionSystem.Instance.AddReaction(effectAction);
            }
            yield return null;
        }
    }
}