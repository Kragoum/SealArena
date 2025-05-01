using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Views
{
    public class CardCollection
    {
        public event Action<int> OnCollectionChanged;
        
        private readonly List<Card> _cards = new();
        public int Count => _cards.Count;
        public void Add(Card card)
        {
            _cards.Add(card);
            OnCollectionChanged?.Invoke(_cards.Count);
        }
        public void AddPile(CardCollection card)
        {
            _cards.AddRange(card._cards);
            OnCollectionChanged?.Invoke(_cards.Count);
        }
        public void Clear()
        {
            _cards.Clear();
            OnCollectionChanged?.Invoke(_cards.Count);
        }
        public IEnumerator<Card> GetEnumerator() => _cards.GetEnumerator(); 
        public void Remove(Card card)
        {
            _cards.Remove(card);
            OnCollectionChanged?.Invoke(_cards.Count);
        }
        public Card Draw()
        {
            if(_cards.Count == 0) return null;
            var randIdx = UnityEngine.Random.Range(0, _cards.Count);
            var selectedCard = _cards[randIdx];
            _cards.RemoveAt(randIdx);
            OnCollectionChanged?.Invoke(_cards.Count);
            return selectedCard;
        }
    }
}