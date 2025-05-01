using System.Collections;
using System.Collections.Generic;
using _Scripts.GameActions;
using _Scripts.Views;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Systems
{
    public class CardSystem : Singleton<CardSystem>
    {
        [SerializeField] private HandView handView;
        [SerializeField] private CardPileView drawPile;
        [SerializeField] private CardPileView discardPile;
        
        private readonly CardCollection _drawCollection = new();
        private readonly CardCollection _discardCollection = new();
        private readonly CardCollection _hand = new();

        private void OnEnable()
        {
            ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
            ActionSystem.AttachPerformer<RefillGA>(RefillPerfomer);
            ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
            ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        }

        public void Setup(List<CardData> deckData)
        {
            foreach (var cardData in deckData)
            {
                Card card = new(cardData);
                _drawCollection.Add(card);
            }
            drawPile.UpdateQuantity(deckData.Count);
            discardPile.UpdateQuantity(0);
            _drawCollection.OnCollectionChanged += drawPile.UpdateQuantity;
            _discardCollection.OnCollectionChanged += discardPile.UpdateQuantity;
        }
        
        private void OnDisable()
        {
            ActionSystem.DetachPerformer<DrawCardsGA>();
            ActionSystem.DetachPerformer<RefillGA>();
            ActionSystem.DetachPerformer<DiscardAllCardsGA>();
            ActionSystem.DetachPerformer<PlayCardGA>();
        }

        // Performers
        private IEnumerator DrawCardsPerformer(DrawCardsGA action)
        {
            int actualAmount = Mathf.Min(action.Amount, _drawCollection.Count);
            for (int i = 0; i < actualAmount; i++)
            {
                yield return DrawCard();
            }
        }
        private IEnumerator RefillPerfomer(RefillGA action)
        {
            _drawCollection.AddPile(_discardCollection);
            _discardCollection.Clear();
            yield return null;
        }
        private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA action)
        {
            foreach (var card in _hand)
            {
                _discardCollection.Add(card);
                CardView cardView = handView.RemoveCard(card);
                yield return DiscardCard(cardView);
            }
            _hand.Clear();
        }

        private IEnumerator PlayCardPerformer(PlayCardGA action)
        {
            _hand.Remove(action.card);
            CardView cardView = handView.RemoveCard(action.card);
            yield return DiscardCard(cardView);
            foreach (var effect in action.card.Effects())
            {
                var performEffectGA = new PerformEffectGA(effect);
                ActionSystem.Instance.AddReaction(performEffectGA);
            }
        }
        
        // Helpers        
        private IEnumerator DrawCard()
        {
            Card card = _drawCollection.Draw();
            _hand.Add(card);
            CardView cardView =
                CardViewCreator.Instance.CreateCardView(card, drawPile.transform.position, drawPile.transform.localRotation);
            yield return handView.AddCard(cardView);
        }

        private IEnumerator DiscardCard(CardView cardView)
        {
            cardView.transform.DOScale(Vector3.zero, 0.15f);
            Tween tween = cardView.transform.DOMove(discardPile.transform.position, 0.15f);
            yield return tween.WaitForCompletion();
            _discardCollection.Add(cardView.Card);
            Destroy(cardView.gameObject);
        }
    }
}