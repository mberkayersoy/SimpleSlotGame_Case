using UnityEngine;
using Zenject;

public class ISpinStateInstaller : MonoInstaller<ISpinStateInstaller>
{
    [SerializeField] private ReelsController _reelsController;

    public override void InstallBindings()
    {
        Container.Bind<ISpinState>().FromInstance(_reelsController).AsSingle().NonLazy();
    }
}
