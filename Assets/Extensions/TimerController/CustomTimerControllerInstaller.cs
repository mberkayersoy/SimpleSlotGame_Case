using MyExtensions.TimerController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CustomTimerControllerInstaller : MonoInstaller<CustomTimerControllerInstaller>
{
    [SerializeField] private CustomTimerController _timerController;

    public override void InstallBindings()
    {
        Container.Bind<CustomTimerController>().FromInstance(_timerController).AsSingle().NonLazy();
    }
}
