using System.Collections.Generic;
using _Scripts.Data;
using SerializeReferenceEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Model
{
    [CreateAssetMenu(fileName = "BossCardData", menuName = "Data/BossCard")]
    public class BossCardData : ScriptableObject
    {
        [field: SerializeField] public Sprite illustration;
        [field: SerializeField] public int weakActions;
        [field: SerializeField] public int strongActions;
        [field: SerializeField] public string title;
        [SerializeReference, SR] public List<PlainEffect> effects;
        [field: SerializeField] public string description;
    }
}