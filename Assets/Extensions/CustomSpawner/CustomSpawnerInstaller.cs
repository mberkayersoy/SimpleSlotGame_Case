using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.CustomSpawner
{
    public class CustomSpawnerInstaller : MonoBehaviour
    {
        /*public abstract class CustomSpawnerInstaller<T, TCustomSpawner> : MonoInstaller<CustomSpawnerInstaller<T, TCustomSpawner>>
            where TCustomSpawner : CustomSpawner<T>
        {
            [SerializeField] protected TCustomSpawner _customSpawner;

            public override void InstallBindings()
            {
                Container.Bind<CustomSpawner<T>>().FromInstance(_customSpawner).AsSingle().NonLazy();
            }
        }*/
    }

}
