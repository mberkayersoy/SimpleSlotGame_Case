using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class UIQuitGameButton : MonoBehaviour
{
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _quitButton.onClick.AddListener(OnClickQuitButton);
    }

    private void OnClickQuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnDestroy()
    {
        _quitButton.onClick.RemoveListener(OnClickQuitButton);
    }
}
