using _Scripts.Model;

namespace _Scripts.GameActions
{
    public class BossCardGA : GameAction
    {
        public BossCard card {get; set;}

        public BossCardGA(BossCard card)
        {
            this.card = card;
        }
    }
}