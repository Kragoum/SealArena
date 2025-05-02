using System.Collections;
using System.Collections.Generic;
using _Scripts.GameActions;
using _Scripts.Views;
using TMPro;
using UnityEngine;

namespace _Scripts.Systems
{
    
    
    public class ManaSystem : Singleton<ManaSystem>
    {
        [SerializeField] private ManaView manaView;
        private int _maximumMana;
        private int _playerMana;

        public bool IsPlayable(Card card)
        {
            return card.Mana + _playerMana <=  _maximumMana;
        }

        private void OnEnable()
        {
            ActionSystem.AttachPerformer<ResetManaGA>(ResetManaPerformer);
            ActionSystem.SubscribeReaction<PlayCardGA>(UpdatePlayerMana, ReactionTiming.PRE);
        }

        public void Setup(int manaMax)
        {
            _maximumMana = manaMax;
            _playerMana = 0;
            manaView.UpdatePlayerMana(_playerMana);
            manaView.UpdateMaximumMana(_maximumMana);
        }

        private void OnDisable()
        {
            ActionSystem.DetachPerformer<ResetManaGA>();
            ActionSystem.UnsubscribeReaction<PlayCardGA>(UpdatePlayerMana, ReactionTiming.PRE);
        }

        private void UpdatePlayerMana(PlayCardGA action)
        {
            _playerMana += action.card.Mana;
            manaView.UpdatePlayerMana(_playerMana);
        }
        private IEnumerator ResetManaPerformer(ResetManaGA action)
        {
            _playerMana = 0;
            manaView.UpdatePlayerMana(0);
            yield return null;
        }
    }
}