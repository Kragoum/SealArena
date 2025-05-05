namespace _Scripts.GameActions
{
    public class PlayCardGA : GameAction
    {
        public Card card {get; set;}

        public PlayCardGA(Card card)
        {
            this.card = card;
        }
    }
}