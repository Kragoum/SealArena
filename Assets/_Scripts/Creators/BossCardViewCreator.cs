using _Scripts.Model;
using DG.Tweening;
using UnityEngine;

public class BossCardViewCreator : Singleton<BossCardViewCreator>
{
    [SerializeField] private BossCardView cardViewPrefab;
    public BossCardView CreateBossCardView(BossCard card, Vector3 position, Quaternion rotation)
    {
        var cardView = Instantiate(cardViewPrefab, position, rotation);
        cardView.Setup(card);
        return cardView;
    }
}
