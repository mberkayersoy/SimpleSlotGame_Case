using Zenject;

public class ISpinHandlerInstaller : MonoInstaller<ISpinHandlerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<SpinController>().AsSingle().NonLazy();
        Container.Bind<ISpinHandler>().FromResolveGetter<SpinController>(handler => handler);
    }
}
