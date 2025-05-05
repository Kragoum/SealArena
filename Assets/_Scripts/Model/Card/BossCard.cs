using System.Collections.Generic;
using _Scripts.Data;
using SerializeReferenceEditor;
using TMPro;
using UnityEngine;

namespace _Scripts.Model
{
    public class BossCard
    {
        private readonly BossCardData _data;
        public Sprite illustration => _data.illustration;
        public int weakActions => _data.weakActions;
        public int strongActions => _data.strongActions;
        public string title => _data.title;
        public string description => _data.description;

        public BossCard(BossCardData data)
        {
            _data = data;
        }
        
        public IEnumerable<PlainEffect> Effects()
        {
            return _data.effects;
        }
    }
}