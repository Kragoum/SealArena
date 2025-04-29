using System.Collections.Generic;
using _Scripts.GameActions;
using _Scripts.Systems;
using UnityEngine;

/// <summary>
/// THIS IS A THROW AWAY CODE
/// </summary>
public class TestSystem : MonoBehaviour
{
    
    [SerializeField] private List<CardData> deck;

    private void Start()
    {
        CardSystem.Instance.Setup(deck);
    }
    
    [SerializeField] private HandView handView;
    [SerializeField] private CardData cardData;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            ActionSystem.Instance.Perform(new DrawCardsGA(1));
        }
    }
}
