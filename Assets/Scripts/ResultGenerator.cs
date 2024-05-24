using System.Collections.Generic;
using System.Linq;
using System;
using Random = System.Random;
public class ResultGenerator
{
    private int[] _indexes = new int[100];
    private ResultData[] _allSpinResults = new ResultData[100];
    private Dictionary<int, (int, int)[]> _perToIntervalDict = new Dictionary<int, (int, int)[]>();
    private Dictionary<string, ResultData> _createdResultsDic;
    public ResultData[] AllSpinResults { get => _allSpinResults; set => _allSpinResults = value; }
    public ResultGenerator(Dictionary<string, ResultData> createdResults)
    {
        _createdResultsDic = createdResults;
        Initialize();
    }
    private void Initialize()
    {
        SetIndexes();
        SetPerToIntervalData();
        SetResults();
    }
    private void SetIndexes()
    {
        for (int i = 0; i < _indexes.Length; i++)
        {
            _indexes[i] = i;
        }
    }
    private void SetPerToIntervalData()
    {
        foreach (var result in _createdResultsDic)
        {
            if (!_perToIntervalDict.ContainsKey(result.Value.ChancePer))
            {
                _perToIntervalDict.Add(result.Value.ChancePer, CalculateSpinIntervals(result.Value.ChancePer));
            }
        }
    }
    private void SetResults()
    {
        foreach (var result in _createdResultsDic)
        {
            PlaceResults(result.Value);
        }
        _indexes = null;
    }
    private void PlaceResults(ResultData result)
    {
        if (_perToIntervalDict.TryGetValue(result.ChancePer, out (int, int)[] resultIntervals))
        {
            foreach (var interval in resultIntervals)
            {
                List<int> availableIndexes = GetAvailableIndexes(interval);
                if (availableIndexes.Count > 0)
                {
                    int randomIndex = availableIndexes[new Random().Next(0, availableIndexes.Count)];
                    _allSpinResults[randomIndex] = result;
                    _indexes[randomIndex] = -1;
                }
            }
        }
        else
        {
            Console.Error.WriteLine("Key not found!");
        }
    }

    private List<int> GetAvailableIndexes((int, int) range)
    {
        List<int> availableIndexes = new List<int>();
        for (int i = range.Item1; i <= range.Item2; i++)
        {
            if (_indexes[i] != -1) availableIndexes.Add(_indexes[i]);
        }
        if (availableIndexes.Count == 0)    
        {
            Dictionary<int, ResultData> _indexToResult = new Dictionary<int, ResultData>();
            int heighestPer = GetHighestPer();
            for (int i = range.Item1; i <= range.Item2; i++)
            {
                if (_allSpinResults[i].ChancePer == heighestPer)
                {
                    _indexToResult.Add(i, _allSpinResults[i]);
                }
            }

            var replace = _indexToResult.ElementAt(new Random().Next(_indexToResult.Count));
            ReplaceIndex(replace.Value);
            availableIndexes.Add(replace.Key);
        }
        return availableIndexes;
    }
    private int GetHighestPer()
    {
        int heighestPer = 0;
        foreach (var item in _perToIntervalDict)
        {
            if (heighestPer <= item.Key) heighestPer = item.Key;
        }
        return heighestPer;
    }
    private void ReplaceIndex(ResultData replacingElement)
    {
        List<int> emptyIndexes = new List<int>();

        foreach (var item in _indexes)
        {
            if (item != -1)
            {
                emptyIndexes.Add(item);
            }
        }
        int emptyty = new Random().Next(0, emptyIndexes.Count);
        _allSpinResults[emptyIndexes[emptyty]] = replacingElement;
        _indexes[emptyIndexes[emptyty]] = -1;
    }

    /// <param name="percentage">The percentage of the total spins for which to calculate intervals.</param>
    /// <param name="totalSpins">The total number of spins.</param>
    /// <returns>A list of tuples representing the start and end indices of each spin interval.</returns>
    /// <remarks>
    /// This method calculates spin intervals based on a given percentage of the total spins. It ensures that the intervals
    /// are as balanced as possible, even if the total number of spins is not evenly divisible by the percentage.
    /// </remarks>
    private (int, int)[] CalculateSpinIntervals(int percentage, int totalSpins = 100)
    {
        (int, int)[] resultIntervals = new (int, int)[percentage];
        int spinsPerInterval = totalSpins / percentage;
        int remainingSpins = totalSpins % percentage;
        int start = 0;
        for (int i = 0; i < percentage; i++)
        {
            int end = start + spinsPerInterval - 1;
            // Check if there are remaining spins to distribute
            if (remainingSpins > 0)
            {
                end++;
                remainingSpins--;
            }
            resultIntervals[i] = (start, end);
            start = end + 1;
        }
        return resultIntervals;
    }
}