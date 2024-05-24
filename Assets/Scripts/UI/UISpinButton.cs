using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISpinButton : MonoBehaviour, ISpinActivator
{
    [SerializeField] private Button _spinButton;
    [Inject] private readonly ISpinState _spinState;

    public event Action SpinRequested;

    private void Awake()
    {
        _spinButton.onClick.AddListener(OnClickSpinButton);
        _spinState.SpinFinished += ActivateButton;
        _spinState.SpinStarted += DeActivateButton;
    }
    private void OnClickSpinButton()
    {
        SpinRequested?.Invoke();
    }
    private void SetButtonInteraction(bool canClick)
    {
        _spinButton.interactable = canClick;
    }
    private void DeActivateButton()
    {
        SetButtonInteraction(false);
    }
    private void ActivateButton()
    {
        SetButtonInteraction(true);
    }
    private void OnDestroy()
    {
        _spinButton.onClick.RemoveListener(OnClickSpinButton);
        _spinState.SpinFinished -= ActivateButton;
        _spinState.SpinStarted -= DeActivateButton;
    }
}
