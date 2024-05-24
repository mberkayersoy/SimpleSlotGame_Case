using UnityEngine;
using Zenject;

public class ISpinHandlerInstaller : MonoInstaller<ISpinHandlerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<SpinController>().AsSingle();
        Container.Bind<ISpinHandler>().FromResolveGetter<SpinController>(handler => handler);
    }
}
