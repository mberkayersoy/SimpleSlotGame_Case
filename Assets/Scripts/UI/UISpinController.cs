using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpinController : MonoBehaviour
{
    [SerializeField] private Button _spinButton;
    [SerializeField] private Color _canSpinColor;
    [SerializeField] private Color _stopSpinColor;
    [SerializeField] private bool _canClick;
    public Action Spined;

    private void Awake()
    {
        _spinButton.onClick.AddListener(OnClickSpinButton);
    }

    private void OnClickSpinButton()
    {
        Spined?.Invoke();
    }

    private void SetButtonInteraction()
    {
        if (_canClick)
        {
            _spinButton.interactable = true;
        }
        else
        {
            _spinButton.interactable = false;
        }
    }
}
