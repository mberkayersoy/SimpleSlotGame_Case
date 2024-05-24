using System;
using UnityEngine;

[Serializable]
public class ResultData
{
    [Range(0, 100)] private int _chancePer;
    private SlotSymbolData[] _resultSymbolsData;
    private string _resultName;
    private bool _isPaying;
    private bool _delayNeeded;
    public int ChancePer { get => _chancePer; set => _chancePer = value; }
    public SlotSymbolData[] ResultSymbolsData { get => _resultSymbolsData; set => _resultSymbolsData = value; }
    public string ResultName { get => _resultName; set => _resultName = value; }
    public bool IsPaying { get => _isPaying; private set => _isPaying = value; }
    public bool IsDelayNeeded { get => _delayNeeded; private set => _delayNeeded = value; }

    public ResultData(int chancePer, SlotSymbolData[] resultSymbols, string resultName, bool isPaying, bool isDelayNeeded)
    {
        _chancePer = chancePer;
        _resultSymbolsData = resultSymbols;
        _resultName = resultName;
        _isPaying = isPaying;
        _delayNeeded = isDelayNeeded;
    }
}
