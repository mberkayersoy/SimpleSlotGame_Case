using UnityEngine;
using Zenject;

public class SpinHandlerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SpinHandler>().AsSingle();
    }
}
