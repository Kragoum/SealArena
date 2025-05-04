using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Views
{
    public class CardCollection<T>
    {
        public event Action<int> OnCollectionChanged;
        
        private readonly List<T> _cards = new();
        public int Count => _cards.Count;
        public void Add(T card)
        {
            _cards.Add(card);
            OnCollectionChanged?.Invoke(_cards.Count);
        }
        public void AddPile(CardCollection<T> card)
        {
            _cards.AddRange(card._cards);
            OnCollectionChanged?.Invoke(_cards.Count);
        }
        public void Clear()
        {
            _cards.Clear();
            OnCollectionChanged?.Invoke(_cards.Count);
        }
        public IEnumerator<T> GetEnumerator() => _cards.GetEnumerator(); 
        public void Remove(T card)
        {
            _cards.Remove(card);
            OnCollectionChanged?.Invoke(_cards.Count);
        }
        public T Draw()
        {
            if(_cards.Count == 0) return default;
            var randIdx = UnityEngine.Random.Range(0, _cards.Count);
            var selectedCard = _cards[randIdx];
            _cards.RemoveAt(randIdx);
            OnCollectionChanged?.Invoke(_cards.Count);
            return selectedCard;
        }
    }
}