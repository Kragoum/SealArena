using System.Collections.Generic;
using _Scripts.GameActions;
using UnityEngine;

namespace _Scripts.Systems
{
    public class GameSetupSystem : MonoBehaviour
    {
        [SerializeField] private List<CardData> deck;

        private void Start()
        {
            CardSystem.Instance.Setup(deck);
            DrawCardsGA drawCardsGa = new(5);
            ActionSystem.Instance.Perform(drawCardsGa);
        }
    }
}