using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.CustomSpawner
{
    public interface ICustomSpawner<T>
    {
        public void SpawnCustomObject(T customObject);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CustomSpawner<T> : MonoBehaviour
    {
        [SerializeField] protected T[] _customObjects;
        [SerializeField] protected float _readyTime = 4f;
        protected float _remainingTime;
        protected readonly HashSet<ICustomSpawner<T>> _customSpawners = new HashSet<ICustomSpawner<T>>();

        protected void Awake()
        {
            _remainingTime = _readyTime;
        }
        protected abstract T GetObject();

        private void Update()
        {
            _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0)
            {
                foreach (var spawner in _customSpawners)
                {
                    spawner.SpawnCustomObject(GetObject());
                }
                _remainingTime = _readyTime;
            }
        }

        public void Register(ICustomSpawner<T> customSpawner)
        {
            _customSpawners.Add(customSpawner);
        }

        public void UnRegister(ICustomSpawner<T> customSpawner)
        {
            _customSpawners.Remove(customSpawner);
        }
    }
}
