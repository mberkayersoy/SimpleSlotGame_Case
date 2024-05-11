using MyExtensions.ObjectPool;
using MyExtensions.TimerController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolManagerInstaller : MonoInstaller<PoolManagerInstaller>
{
    [SerializeField] private PoolManager _poolManager;

    public override void InstallBindings()
    {
        Container.Bind<PoolManager>().FromInstance(_poolManager).AsSingle().NonLazy();
    }
}
