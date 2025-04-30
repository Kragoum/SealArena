using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Extensions;
using _Scripts.GameActions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Systems
{
    public class CardSystem : Singleton<CardSystem>
    {
        [SerializeField] private HandView handView;
        [SerializeField] private Transform drawPilePoint;
        [SerializeField] private Transform discardPilePoint;
        
        private readonly List<Card> drawPile = new();
        private readonly List<Card> discardPile = new();
        private readonly List<Card> hand = new();

        private void OnEnable()
        {
            ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
            ActionSystem.AttachPerformer<RefillGA>(RefillPerfomer);
            ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
            ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
            
            ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
        }

        public void Setup(List<CardData> deckData)
        {
            foreach (var cardData in deckData)
            {
                Card card = new(cardData);
                drawPile.Add(card);
            }
        }
        
        private void OnDisable()
        {
            ActionSystem.DetachPerformer<DrawCardsGA>();
            ActionSystem.DetachPerformer<RefillGA>();
            ActionSystem.DetachPerformer<DiscardAllCardsGA>();
            ActionSystem.DetachPerformer<PlayCardGA>();
            
            ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
        }

        // Performers
        private IEnumerator DrawCardsPerformer(DrawCardsGA action)
        {
            int actualAmount = Mathf.Min(action.Amount, drawPile.Count);
            for (int i = 0; i < actualAmount; i++)
            {
                yield return DrawCard();
            }
        }
        private IEnumerator RefillPerfomer(RefillGA action)
        {
            drawPile.AddRange(discardPile);
            discardPile.Clear();
            yield return null;
        }
        private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA action)
        {
            foreach (var card in hand)
            {
                discardPile.Add(card);
                CardView cardView = handView.RemoveCard(card);
                yield return DiscardCard(cardView);
            }
            hand.Clear();
        }

        private IEnumerator PlayCardPerformer(PlayCardGA action)
        {
            hand.Remove(action.card);
            CardView cardView = handView.RemoveCard(action.card);
            yield return DiscardCard(cardView);
            // TODO Perform effect
        }

        // Reactions
        private void EnemyTurnPostReaction(EnemyTurnGA action)
        {
            ActionSystem.Instance.AddReaction(new DiscardAllCardsGA());
            ActionSystem.Instance.AddReaction(new RefillGA());
            ActionSystem.Instance.AddReaction(new DrawCardsGA(5));
        }
        
        // Helpers        
        private IEnumerator DrawCard()
        {
            Card card = drawPile.Draw();
            hand.Add(card);
            CardView cardView =
                CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.localRotation);
            yield return handView.AddCard(cardView);
        }

        private IEnumerator DiscardCard(CardView cardView)
        {
            cardView.transform.DOScale(Vector3.zero, 0.15f);
            Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
            yield return tween.WaitForCompletion();
            discardPile.Add(cardView.Card);
            Destroy(cardView.gameObject);
        }
    }
}