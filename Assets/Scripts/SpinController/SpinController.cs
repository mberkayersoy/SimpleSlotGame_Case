using System;

public class SpinController : ISpinHandler
{
    private int _currentSpin;
    private ResultData[] _spinResults;
    public event Action<ResultData> NextSpinResultConcluded;
    public event Action AllSpinResultsDone;

    public void OnNextSpinResult()
    {
        SetCurrentSpin();
        NextSpinResultConcluded?.Invoke(_spinResults[_currentSpin]);
    }
    private void SetCurrentSpin()
    {
        _currentSpin++;
        if (_currentSpin > _spinResults.Length)
        {
            AllSpinResultsDone?.Invoke();
        }
    }
    public int GetCurrentSpin()
    {
        return _currentSpin;
    }
    public void SetSpinResults(ResultData[] newSpinResults, int currentSpin = -1)
    {
        _spinResults = newSpinResults;
        _currentSpin = currentSpin;
    }
}
