using UnityEngine;
using Zenject;

public class ReelsControllerInstaller : MonoInstaller<ReelsControllerInstaller>
{
    [SerializeField] private ReelsController _uiReelsController;

    public override void InstallBindings()
    {
        if (_uiReelsController == null)
        {
            _uiReelsController = GetComponent<ReelsController>();
        }
        Container.Bind<ReelsController>().FromInstance(_uiReelsController).AsSingle().NonLazy();
    }

}
