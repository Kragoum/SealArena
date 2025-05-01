using _Scripts.Data;

namespace _Scripts.GameActions
{
    public class PerformEffectGA : GameAction
    {
        public PlainEffect Effect { get; private set; }

        public PerformEffectGA(PlainEffect effect)
        {
            Effect = effect;
        }
    }
}