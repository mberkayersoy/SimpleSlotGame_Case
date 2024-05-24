using UnityEngine;
using UnityEngine.UI;
using System;
public class UIExitGameButton : MonoBehaviour, IGameExit
{
    [SerializeField] private Button _exitGameButton;
    public event Action GameExited;
    private void Awake()
    {
        _exitGameButton.onClick.AddListener(OnClickQuitButton);
    }
    private void OnClickQuitButton()
    {
        GameExited?.Invoke();
    }
    private void OnDestroy()
    {
        _exitGameButton.onClick.RemoveListener(OnClickQuitButton);
    }
}
