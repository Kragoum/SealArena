using System.Collections.Generic;
using _Scripts.GameActions;
using UnityEngine;

namespace _Scripts.Data
{
    public class DrawEffect : PlainEffect
    {
        [field: SerializeField] public int Amount;
        public override IEnumerable<GameAction> GameActionsEffect()
        {
            yield return new DrawCardsGA(Amount);
        }
    }
}