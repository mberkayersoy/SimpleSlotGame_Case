using System;
public class SpinController : ISpinHandler
{
    private int _currentSpin;
    private ResultData[] _spinResults;

    public event Action<ResultData> NextSpinResultConcluded;
    public event Action AllSpinResultsDone;

    public SpinController()
    {
        LoadSavedData();
    }
    public void OnNextSpinResult()
    {
        SetCurrentSpin();
        NextSpinResultConcluded?.Invoke(_spinResults[_currentSpin]);
        JsonSaver.SaveData(_currentSpin, JsonSaver.CURRENT_SPIN_FILE_PATH);
    }
    private void SetCurrentSpin()
    {
        _currentSpin++;
        if (_currentSpin > _spinResults.Length - 1)
        {
            AllSpinResultsDone?.Invoke();
        }
    }
    public void SetNewSpinResults(ResultData[] newSpinResults, int currentSpin = -1)
    {
        _spinResults = newSpinResults;
        _currentSpin = currentSpin;
        SaveCurrentData();
    }

    private void LoadSavedData()
    {
        _spinResults = JsonSaver.LoadData<ResultData[]>(JsonSaver.ALL_SPIN_RESULTS_FILE_PATH);
        _currentSpin = JsonSaver.LoadData<int>(JsonSaver.CURRENT_SPIN_FILE_PATH);
    }
    private void SaveCurrentData()
    {
        JsonSaver.SaveData(_spinResults, JsonSaver.ALL_SPIN_RESULTS_FILE_PATH);
        JsonSaver.SaveData(_currentSpin, JsonSaver.CURRENT_SPIN_FILE_PATH);
    }
}
