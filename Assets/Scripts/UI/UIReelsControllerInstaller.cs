using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIReelsControllerInstaller : MonoInstaller<UIReelsControllerInstaller>
{
    [SerializeField] private UIReelsController _uiReelsController;

    public override void InstallBindings()
    {
        if (_uiReelsController == null)
        {
            _uiReelsController = GetComponent<UIReelsController>();
        }
        Container.Bind<UIReelsController>().FromInstance(_uiReelsController).AsSingle().NonLazy();
    }

}
