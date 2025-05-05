using System.Collections.Generic;

namespace _Scripts.Data
{
    [System.Serializable]
    public abstract class PlainEffect
    {
        public abstract IEnumerable<GameAction> GameActionsEffect();
    }
}