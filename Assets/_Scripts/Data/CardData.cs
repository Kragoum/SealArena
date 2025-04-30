using System.Collections.Generic;
using _Scripts.Data;
using UnityEngine;
using SerializeReferenceEditor;

[CreateAssetMenu(fileName = "CardData", menuName = "Data/Card")]
public class CardData : ScriptableObject
{
    [SerializeReference, SR] public List<PlainEffect> Effects; 
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Mana { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
}
