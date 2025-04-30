using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private List<CardView> cards = new();
    private float animationDuration = 0.15f;
    public IEnumerator AddCard(CardView cardView)
    {
        cards.Add(cardView);
        yield return UpdateCardPositions(animationDuration) ;
    }

    private IEnumerator UpdateCardPositions(float duration)
    {
        if(cards.Count == 0) yield break;
        float cardSpacing = 1f / 10f;
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;

        for (int i = 0; i < cards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
            cards[i].transform.DOMove(splinePosition + splineContainer.transform.position + 0.01f * i * Vector3.back, duration);
            cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }
        yield return new WaitForSeconds(duration);
    }

    public CardView RemoveCard(Card card)
    {
        CardView cardView = GetCardView(card);
        if (cardView == null)
        {
            Debug.LogWarning("CardView not found");
            return null;
        }
        cards.Remove(cardView);
        StartCoroutine(UpdateCardPositions(animationDuration));
        return cardView;
    }

    private CardView GetCardView(Card card)
    {
        return cards.Where(view => view.Card == card).FirstOrDefault(); 
    }
}
