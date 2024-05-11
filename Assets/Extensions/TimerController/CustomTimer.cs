using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Zenject;

namespace MyExtensions.TimerController
{
    public interface ITimer
    {
        public void UpdateTimer(float deltaTime);
        public void StartTimer();
        public void StopTimer();
        public void SetDuration(float duration);
        public Action TimerCompleted { get; set; }
    }

    [Serializable]
    public class CustomTimer : ITimer
    {
        [SerializeField] private float _duration;
        [SerializeField] private bool _isLoop;
        [SerializeField] private float _remainingTime;
        [Inject] private CustomTimerController _timerContorller;
        private bool _isActive;
        public bool IsActive { get => _isActive; private set => _isActive = value; }
        public Action TimerCompleted { get; set; }

        public CustomTimer(float duration, bool isLoop, CustomTimerController customTimerController)
        {
            _duration = duration;
            _isLoop = isLoop;
            customTimerController.Register(this);
            
        }
        public void UpdateTimer(float deltaTime)
        {
            if (_isActive)
            {
                _remainingTime -= deltaTime;

                if (_remainingTime <= 0)
                {
                    _isActive = _isLoop;
                    if (!_isActive) _timerContorller.Unregister(this);
  
                    _remainingTime = _duration;
                    TimerCompleted?.Invoke();
                }
            }
        }

        public void StartTimer()
        {
            Debug.Log("StartTimer");
            _remainingTime = _duration;
            _isActive = true;
            Debug.Log("StartTimer is active: "+ IsActive);
        }

        public void SetDuration(float duration)
        {
            _duration = Mathf.Max(duration, 0f);
            _remainingTime = _duration;
        }
        public void StopTimer()
        {
            _isActive = false;
        }
    }
}
