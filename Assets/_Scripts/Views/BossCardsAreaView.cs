using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Model;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace _Scripts.Views
{
    public class BossCardsAreaView : MonoBehaviour
    {
        [SerializeField] private Transform cardSpawnPosition;
        [SerializeField] public Transform firstCardPosition;
        [SerializeField] private Vector3 actionCardSpacing;
        [SerializeField] private Vector3 initialspacing;
        

        private BossCardView _bossCard; 
        [SerializeField] private List<GameObject> minorActions = new();
        [SerializeField] private List<GameObject> majorActions = new();
        [SerializeField] private float animationDuration = 0.10f;
        
        public IEnumerator SetBossCard(BossCardView bossCardView)
        {
            _bossCard =  bossCardView;
            _bossCard.transform.position = firstCardPosition.position;
            yield return UpdateCardPositions(animationDuration) ;
        }
        public IEnumerator AddMinorAction(GameObject minorAction)
        {
            minorActions.Add(minorAction);
            yield return UpdateCardPositions(animationDuration) ;
        }
        public IEnumerator AddMajorAction(GameObject majorAction)
        {
            majorActions.Add(majorAction);
            yield return UpdateCardPositions(animationDuration) ;
        }
        private IEnumerator UpdateCardPositions(float duration)
        {
            if (!_bossCard) yield return null;
            else
            {
                var referencePosition = _bossCard.transform.position;
                for (var i = 0; i < minorActions.Count; i++)
                {
                    minorActions[i].transform.DOMove(referencePosition - initialspacing -i*actionCardSpacing, duration);
                }
                for (var i = 0; i < majorActions.Count; i++)
                {
                    majorActions[i].transform.DOMove(referencePosition + initialspacing + i*actionCardSpacing, duration);
                }
                yield return new WaitForSeconds(duration);
            }
        }
        public MonoBehaviour DiscardCard(BossCard card)
        {
            return _bossCard;
        }
    }
}