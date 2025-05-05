using TMPro;
using UnityEngine;

namespace _Scripts.Model
{
    public partial class BossCardView : MonoBehaviour
    {
        [SerializeField] private GameObject wrapper;
        [SerializeField] private SpriteRenderer illustration;
        [SerializeField] private TextMeshPro weakActions;
        [SerializeField] private TextMeshPro strongActions;
        [SerializeField] private TextMeshPro description;
        [SerializeField] private TextMeshPro title;
        public BossCard Card { get; private set; }

        public void Setup(BossCard card)
        {
            Card = card;
            illustration.sprite = card.illustration;
            weakActions.text = card.weakActions.ToString();
            strongActions.text = card.strongActions.ToString();
            description.text = card.description;
            title.text = card.title;
        }
    }
}