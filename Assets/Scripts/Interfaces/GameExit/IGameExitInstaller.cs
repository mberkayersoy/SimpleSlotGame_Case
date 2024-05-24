using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class IGameExitInstaller : MonoInstaller<IGameExitInstaller>
{
    [SerializeField] private UIExitGameButton _exitGameButton;
    public override void InstallBindings()
    {
        Container.Bind<IGameExit>().FromInstance(_exitGameButton).AsSingle().NonLazy();
    }
}
