using UnityEngine;
using Zenject;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SlotGameManager : MonoBehaviour
{
    private ResultGenerator _resultGenerator;

    private IDataService _dataService;
    [Inject] private ISpinHandler _spinHandler;
    [Inject] private IGameExit _iGameExit;
    [Inject] private ISpinActivator _spinActivator;
    private void Start()
    {
        InitializeDataService();
        InitializeNewSpinResults();
        _spinHandler.AllSpinResultsDone += CreateNewSpinResults;
        _spinActivator.SpinRequested += GetNextSpinResult;
        _iGameExit.GameExited += ExitGame;
    }
    private void InitializeDataService()
    {
        _dataService = new JsonDataService();
        if (!_dataService.CheckFileExistince(GameConstantData.ALL_SPIN_RESULTS_FILE_PATH))
        {
            InitializeNewSpinResults();
        }
        else
        {
            LoadExistingSpinResults();
        }
    }
    private void InitializeNewSpinResults()
    {
        var createdResults = _dataService.LoadData<Dictionary<string, ResultData>>(GameConstantData.CREATED_RESULTS_FILE_PATH);
        _resultGenerator = new ResultGenerator(createdResults);
        _dataService.SaveData(GameConstantData.ALL_SPIN_RESULTS_FILE_PATH, _resultGenerator.AllSpinResults);
        _spinHandler.SetSpinResults(_resultGenerator.AllSpinResults, -1);
    }
    private void LoadExistingSpinResults()
    {
        _spinHandler.SetSpinResults(
            _dataService.LoadData<ResultData[]>(GameConstantData.ALL_SPIN_RESULTS_FILE_PATH),
            _dataService.LoadData<int>(GameConstantData.CURRENT_SPIN_FILE_PATH)
        );
    }
    private void CreateNewSpinResults()
    {
        _resultGenerator = new ResultGenerator(_dataService.LoadData<Dictionary<string, ResultData>>(GameConstantData.CREATED_RESULTS_FILE_PATH));
        _spinHandler.SetSpinResults(_resultGenerator.AllSpinResults, 0);
    }
    private void GetNextSpinResult()
    {
        _spinHandler.OnNextSpinResult();
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
        _dataService.SaveData(GameConstantData.CURRENT_SPIN_FILE_PATH, _spinHandler.GetCurrentSpin());
        _spinHandler.AllSpinResultsDone -= CreateNewSpinResults;
        _spinActivator.SpinRequested -= GetNextSpinResult;
        _iGameExit.GameExited -= ExitGame;
    }
}
