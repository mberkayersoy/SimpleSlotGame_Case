using System;
using UnityEngine;

namespace MyExtensions.ObjectPool
{
    public abstract class PoolableComponent : MonoBehaviour
    {
        public event Action<PoolableComponent> ResettedPoolObject;
        public virtual void ResetPoolableObject()
        {
            ResettedPoolObject?.Invoke(this);
        }
    }
}

