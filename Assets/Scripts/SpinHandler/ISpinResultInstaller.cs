using UnityEngine;
using Zenject;

public class ISpinResultInstaller : MonoInstaller<ISpinResultInstaller>
{
    //public override void InstallBindings()
    //{
    //    Container.Bind<SpinHandler>().AsSingle();
    //}
    public override void InstallBindings()
    {
        Container.Bind<SpinController>().AsSingle();
        Container.Bind<ISpinHandler>().FromResolveGetter<SpinController>(handler => handler);
    }
}
