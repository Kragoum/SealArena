using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;
using Object = UnityEngine.Object;

namespace _Scripts.Model.TimeLine
{
    public class TimeLineTokenViewFactory : Singleton<TimeLineTokenViewFactory>
    {
        [SerializeField] private TimeLineTokenView playerTokenPrefab;
        public TimeLineTokenView CreatePlayerTokenView(TimeLineToken card, Vector3 position)
        {
            var tokenView = Instantiate(playerTokenPrefab, position, Quaternion.identity);
            tokenView.transform.localScale = Vector3.zero;
            tokenView.transform.DOScale(Vector3.one, 0.15f);
            tokenView.Setup(card);
            return tokenView;
        }
    }
    public class TimeLineTokenView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer imageSR;
        public ITimeLineToken token { get; private set; }

        public void Setup(ITimeLineToken card)
        {
            imageSR.sprite = token.Image();
        }
    }
    public interface ITimeLineToken
    {
        public int CurrentActionTime();
        public void UpdateActionTime(int time);
        public Sprite Image();
    }
    public class TimeLineToken : ITimeLineToken
    {
        private readonly TimeLineTokenData _data;
        private int _currentActionTime;
        public TimeLineToken(TimeLineTokenData data, int initialActionTime = 0)
        {
            _data = data;
            _currentActionTime = initialActionTime;
        }
        public void UpdateActionTime(int time)
        {
            _currentActionTime += time;
        }
        public int CurrentActionTime()
        {
            return _currentActionTime;
        }
        public Sprite Image() => _data.image;
    }
    public class TimeLineTokenData : ScriptableObject
    {
        [SerializeField] public Sprite image;
    }
    public class TimeLine
    {
        [SerializeField] private int size;

        public void Initialize(int maxSize)
        {
            size = maxSize;
            timeline = new List<ITimeLineToken>[maxSize];
            for (var i = 0; i < maxSize; i++)
            {
                timeline[i] = new List<ITimeLineToken>();
            }
        }
        
        private List<ITimeLineToken>[] timeline;

        public void AddToken(TimeLineToken token, int actionTime)
        {
            timeline[actionTime].Add(token);
        }
        public void RemoveToken(TimeLineToken token)
        {
            timeline.FirstOrDefault(tokens => tokens.Contains(token))?.Remove(token);
        }
        public void MoveToken(ITimeLineToken token, int value)
        {
            var currentValue = token.CurrentActionTime();
            var finalValue = Math.Clamp(currentValue + value, 0, size);
            token.UpdateActionTime(finalValue);
            timeline[currentValue].Remove(token);
            timeline[finalValue].Add(token);
        }
        public bool IsMoveAcceptable(ITimeLineToken token, int value)
        {
            var finalValue = token.CurrentActionTime() + value; 
            return 0 <= finalValue && finalValue <= size;
        }
    }

    public class TimeLineView : MonoBehaviour
    {
        [SerializeField] private List<TimeLineTokenView> tokens = new();
        [SerializeField] private Spline spline;
        [SerializeField] private float animationDuration = 0.15f;
        [SerializeField] private int size;
        
        public void AddToken(TimeLineTokenView cardView)
        {
            tokens.Add(cardView);
        }

        public Vector3 ActionTimePosition(int actionTime)
        {
            return spline.EvaluatePosition((float)size / actionTime);
        }

        public IEnumerator MoveTokenView(TimeLineTokenView cardView, int finalActionTime)
        {
            Vector3 finalPosition = spline.EvaluatePosition((float)size / finalActionTime);
            cardView.transform.DOMove(cardView.transform.position + finalPosition, animationDuration);
            yield return new WaitForSeconds(animationDuration);
        }
    }

    public class TimeLineSystem : Singleton<TimeLineSystem>
    {
        [SerializeField] private TimeLineView timeLineView;
        private TimeLine _timeLine;
        
        public void Setup(int size, List<TimeLineToken> playerTokens)
        {
            _timeLine = new TimeLine();
            _timeLine.Initialize(size);
            foreach (var token in playerTokens)
            {
                _timeLine.AddToken(token, token.CurrentActionTime());
                var tokenView = TimeLineTokenViewFactory.Instance.CreatePlayerTokenView(token, timeLineView.ActionTimePosition(0));
                timeLineView.AddToken(tokenView);
            }
        }
        
        
    }
}