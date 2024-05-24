using System;

public class SpinController : ISpinHandler
{
    private int _currentSpin;
    private ResultData[] _spinResults;

    public event Action<ResultData> NextSpinResultConcluded;
    public event Action AllSpinResultsDone;
    private IDataService _dataService;
    public SpinController()
    {
        _dataService = new JsonDataService();
        LoadSavedData();
    }
    public void OnNextSpinResult()
    {
        SetCurrentSpin();
        NextSpinResultConcluded?.Invoke(_spinResults[_currentSpin]);
        _dataService.SaveData(GameConstantData.CURRENT_SPIN_FILE_PATH, _currentSpin);
    }
    private void SetCurrentSpin()
    {
        _currentSpin++;
        if (_currentSpin > _spinResults.Length)
        {
            AllSpinResultsDone?.Invoke();
        }
    }
    public void SetSpinResults(ResultData[] newSpinResults, int currentSpin = -1)
    {
        _spinResults = newSpinResults;
        _currentSpin = currentSpin;
        SaveCurrentData();
    }

    private void LoadSavedData()
    {
        _spinResults = _dataService.LoadData<ResultData[]>(GameConstantData.ALL_SPIN_RESULTS_FILE_PATH);
        _currentSpin = _dataService.LoadData<int>(GameConstantData.CURRENT_SPIN_FILE_PATH);
    }
    private void SaveCurrentData()
    {
        _dataService.SaveData(GameConstantData.ALL_SPIN_RESULTS_FILE_PATH, _spinResults);
        _dataService.SaveData(GameConstantData.CURRENT_SPIN_FILE_PATH, _currentSpin);
    }

}
