using TMPro;
using UnityEngine;

namespace _Scripts.Views
{
    public class ManaView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer imageSR;
        [SerializeField] private GameObject wrapper;
        [SerializeField] private TMP_Text playerMana;
        [SerializeField] private TMP_Text maximumMana;
    
        public void UpdatePlayerMana(int newMana)
        {
            playerMana.text =  newMana.ToString();
        }

        public void UpdateMaximumMana(int newMana)
        {
            maximumMana.text = newMana.ToString();
        }
    }
}