using System;
using UnityEngine;

public class SpinHandler
{
    private int _currentSpin;
    private ResultData[] _spinResults;
    public Action AllSpinResultsDone;
    public SpinHandler()
    {
        LoadSavedData();
    }
    public string GetNextSpinResult()
    {
        _currentSpin++;
        if (_currentSpin > _spinResults.Length - 1)
        {
            AllSpinResultsDone?.Invoke();
            RefreshSpinResults();
        }
        JsonSaver.SaveData(_currentSpin, JsonSaver.CURRENT_SPIN_FILE_PATH);
        return _spinResults[_currentSpin].ResultName;
    }
    public void RefreshSpinResults()
    {
        _spinResults = JsonSaver.LoadData<ResultData[]>(JsonSaver.ALL_SPIN_RESULTS_FILE_PATH);
        _currentSpin = 0; // Reset Current Spin
        SaveCurrentData();
    }

    private void LoadSavedData()
    {
        _spinResults = JsonSaver.LoadData<ResultData[]>(JsonSaver.ALL_SPIN_RESULTS_FILE_PATH);
        _currentSpin = JsonSaver.LoadData<int>(JsonSaver.CURRENT_SPIN_FILE_PATH);
        Debug.Log(_currentSpin);
    }

    public void SaveCurrentData()
    {
        JsonSaver.SaveData(_spinResults, JsonSaver.ALL_SPIN_RESULTS_FILE_PATH);
        JsonSaver.SaveData(_currentSpin, JsonSaver.CURRENT_SPIN_FILE_PATH);
    }
}
