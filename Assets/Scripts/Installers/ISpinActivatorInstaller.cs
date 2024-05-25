using UnityEngine;
using Zenject;

public class ISpinActivatorInstaller : MonoInstaller<ISpinActivatorInstaller>
{
    [SerializeField] private UISpinButton _spinButton;
    public override void InstallBindings()
    {
        Container.Bind<ISpinActivator>().FromInstance(_spinButton).AsSingle().NonLazy();
    }
}
