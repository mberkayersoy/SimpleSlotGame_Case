using System.IO;
using UnityEngine;
using Zenject;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SlotGameManager : MonoBehaviour
{
    private ResultGenerator _resultgenerator;
    [Inject] private ISpinHandler _spinhandler;
    [Inject] private IGameExit _gameExit;
    [Inject] private ISpinActivator _spinActivator;
    private void Awake()
    {
        if (!File.Exists(JsonSaver.ALL_SPIN_RESULTS_FILE_PATH))
        {
            _resultgenerator = new ResultGenerator();
            _spinhandler.SetNewSpinResults(_resultgenerator.AllSpinResults, -1);

        }
        _spinhandler.AllSpinResultsDone += CreateNewSpinResults;
        _spinActivator.SpinRequested += GetNextSpinResult;
        _gameExit.GameExited += ExitGame;
    }

    private void GetNextSpinResult()
    {
        _spinhandler.OnNextSpinResult();
    }
    private void CreateNewSpinResults()
    {
        _resultgenerator = new ResultGenerator();
        _spinhandler.SetNewSpinResults(_resultgenerator.AllSpinResults, -1);
    }
    private void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnDestroy()
    {
        _spinhandler.AllSpinResultsDone -= CreateNewSpinResults;
        _spinActivator.SpinRequested -= GetNextSpinResult;
        _gameExit.GameExited -= ExitGame;
    }
}
