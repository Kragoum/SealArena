using System.Collections.Generic;
using _Scripts.GameActions;
using _Scripts.Model;
using UnityEngine;

namespace _Scripts.Systems
{
    public class GameSetupSystem : MonoBehaviour
    {
        [SerializeField] private List<BossCardData> bossDeck;
        [SerializeField] private List<CardData> deck;
        [SerializeField] private int initialMaximumMana;
        
        private void Start()
        {
            EnemySystem.Instance.Setup(bossDeck);
            CardSystem.Instance.Setup(deck);
            DrawCardsGA drawCardsGa = new(5);
            ActionSystem.Instance.Perform(drawCardsGa);
            ManaSystem.Instance.Setup(initialMaximumMana);
        }
    }
}