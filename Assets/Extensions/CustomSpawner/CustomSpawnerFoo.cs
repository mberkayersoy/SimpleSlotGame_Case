using MyExtensions.RuntimeSet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.CustomSpawner
{
    /// <summary>
    /// Example class for CustomSpawner
    /// </summary>
    public class CustomSpawnerFoo : MonoBehaviour, ICustomSpawner<Foo>
    {
        [SerializeField] private CustomSpawner<Foo> _fooSpawner;

        private void Awake()
        {
            _fooSpawner.Register(this);
        }
        // Objects will arrive within the _readyTime interval specified in CustomSpawner.
        public void SpawnCustomObject(Foo foo)
        {
            Instantiate(foo);
            //_poolManager.Spawn();
        }

        private void OnDestroy()
        {
            _fooSpawner.UnRegister(this);
        }
    }

}
