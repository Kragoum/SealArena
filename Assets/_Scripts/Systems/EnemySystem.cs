using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.GameActions;
using _Scripts.Model;
using _Scripts.Views;
using DG.Tweening;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [SerializeField] private BossCardsAreaView areaView;
    [SerializeField] private CardPileView drawPile;
    [SerializeField] private CardPileView discardPile;
    
    private readonly CardCollection<BossCard> _drawCollection = new();
    private readonly CardCollection<BossCard> _discardCollection = new();
    private BossCard _currentBossCard;

    public void Setup(List<BossCardData> deckData)
    {
        foreach (var cardData in deckData)
        {
            var card = new BossCard(cardData);
            _drawCollection.Add(card);
        }
        drawPile.UpdateQuantity(deckData.Count);
        discardPile.UpdateQuantity(0);
        _drawCollection.OnCollectionChanged += drawPile.UpdateQuantity;
        _discardCollection.OnCollectionChanged += discardPile.UpdateQuantity;
        StartCoroutine(DrawNextCard());
    }

    public void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(ExecuteTurn);
    }

    public void OnDisable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(ExecuteTurn);
    }

    public IEnumerator ExecuteTurn(EnemyTurnGA action)
    {
        PerformActions();
        yield return DiscardAllCards();
        yield return DrawNextCard();
    }

    private void PerformActions()
    {
        foreach (var effect in _currentBossCard.Effects())
        {
            var performEffectGA = new PerformEffectGA(effect);
            ActionSystem.Instance.AddReaction(performEffectGA);
        }
        //TODO play all actions cards
    }
    
    // Helpers
    private IEnumerator DrawNextCard()
    {
        _currentBossCard = _drawCollection.Draw();
        var curreBossCardView = BossCardViewCreator.Instance.CreateBossCardView(_currentBossCard, Vector3.zero, Quaternion.identity);
        yield return areaView.SetBossCard(curreBossCardView);

        for (int i = 0; i < _currentBossCard.weakActions; i++)
        {
            //TODO Draw card
            //TODO Create view
        }
        for (int i = 0; i < _currentBossCard.strongActions; i++)
        {
            //TODO Draw card
            //TODO Create view
        }
    }
    private IEnumerator DiscardAllCards()
    {
        yield return DiscardCard(_currentBossCard);
        
        //TODO discard action cards
        //TODO refactor to avoid code duplication...
    }

    private IEnumerator DiscardCard(BossCard card)
    {
        var cardView = areaView.DiscardCard(card);
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPile.transform.position, 0.15f);
        yield return tween.WaitForCompletion();
        _discardCollection.Add(card);
        _currentBossCard = null;
        Destroy(cardView.gameObject);
    }
}
