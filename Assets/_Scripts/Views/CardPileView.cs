using TMPro;
using UnityEngine;

namespace _Scripts.Views
{
    public class CardPileView : MonoBehaviour
    {
        [SerializeField] private TMP_Text quantity;
        [SerializeField] private SpriteRenderer imageSR;
        [SerializeField] private GameObject wrapper;
        
        public void UpdateQuantity(int newQuantity)
        {
            var newText = newQuantity.ToString();
            if (quantity.text == newText) return;
            quantity.text = newText;
        }
    }
}