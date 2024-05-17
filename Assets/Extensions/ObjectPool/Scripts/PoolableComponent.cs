using System;
using UnityEngine;
using Zenject;

namespace MyExtensions.ObjectPool
{
    [RequireComponent(typeof(GameObjectContext))]
    [RequireComponent(typeof(ZenAutoInjecter))]
    public abstract class PoolableComponent : MonoBehaviour
    {
        public event Action<PoolableComponent> ResettedPoolObject;
        protected virtual void Reset()
        {
            GetComponent<ZenAutoInjecter>().ContainerSource = ZenAutoInjecter.ContainerSources.SceneContext;
        }
        public virtual void ResetPoolableObject()
        {
            ResettedPoolObject?.Invoke(this);
        }
    }
}

