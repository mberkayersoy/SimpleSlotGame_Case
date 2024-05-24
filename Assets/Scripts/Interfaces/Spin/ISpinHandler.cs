using System;

public interface ISpinHandler 
{
    public event Action<ResultData> NextSpinResultConcluded;
    public event Action AllSpinResultsDone;
    public void SetNewSpinResults(ResultData[] _spinResults, int currentSpin);
    public void OnNextSpinResult();
}
