using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.TimerController
{
    public class CustomTimerController : MonoBehaviour
    {
        private HashSet<ITimer> _activeTimers = new HashSet<ITimer>();
        private Queue<ITimer> _timersToAdd = new Queue<ITimer>();
        private Queue<ITimer> _timersToRemove = new Queue<ITimer>();
        private void Update()
        {
            if (_activeTimers != null && _activeTimers.Count > 0)
            {
                foreach (ITimer customTimer in _activeTimers)
                {
                    customTimer.UpdateTimer(Time.deltaTime);
                }
            }

            while (_timersToAdd.Count > 0)
            {
                Debug.Log("Timer added");
                var timerToAdd = _timersToAdd.Dequeue();
                _activeTimers.Add(timerToAdd);
                timerToAdd.StartTimer();
            }

            while (_timersToRemove.Count > 0)
            {
                var timerToRemove = _timersToRemove.Dequeue();
                timerToRemove.StopTimer();
                _activeTimers.Remove(timerToRemove);
            }
        }
        public void Register(ITimer customTimer)
        {
            _timersToAdd.Enqueue(customTimer);
        }
        public void Unregister(ITimer customTimer)
        {
            _timersToRemove.Enqueue(customTimer);
        }
    }
}
