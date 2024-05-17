using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISpinButton : MonoBehaviour
{
    [SerializeField] private Button _spinButton;
    public Action Spined;
    [Inject] private SpinHandler _spinHandler;
    [Inject] UIReelsController _reelsController;
    private void Awake()
    {
        _spinButton.onClick.AddListener(OnClickSpinButton);
        _reelsController.SpinDone += SetButtonInteraction;
    }
    private void OnClickSpinButton()
    {
        _spinHandler.OnSpinResult();
        SetButtonInteraction(false);
    }

    private void SetButtonInteraction(bool canClick)
    {
        _spinButton.interactable = canClick;
    }
    private void OnDestroy()
    {
        _spinButton.onClick.RemoveListener(OnClickSpinButton);
        _reelsController.SpinDone -= SetButtonInteraction;
    }
}
