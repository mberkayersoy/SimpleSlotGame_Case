using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotTestScripts : MonoBehaviour
{
    private int[] _indexes = new int[100];
    [SerializeField] private CustomKeyValuePair<string, int>[] _results = new CustomKeyValuePair<string, int>[100];
    private Dictionary<int, (int, int)[]> _perToIntervalDict = new Dictionary<int, (int, int)[]>();
    [SerializeField] private CustomKeyValuePair<string, int>[] _slotData;

    public CustomKeyValuePair<string, int>[] Results { get => _results; private set => _results = value; }
    public CustomKeyValuePair<string, int>[] SlotData { get => _slotData; private set => _slotData = value; }

    private void Awake()
    {
        SetIndexes();
        GetSlotData(); 
    }

    private void SetIndexes()
    {
        for (int i = 0; i < _indexes.Length; i++)
        {
            _indexes[i] = i;
        }
    }
    private void Start()
    {
        foreach (var result in SlotData)
        {
            if (!_perToIntervalDict.ContainsKey(result.Value))
            {
                _perToIntervalDict.Add(result.Value, CalculateSpinIntervals(result.Value));
            }
        }
        SetResults();
    }

    private void SetResults()
    {
        foreach (var result in SlotData)
        {
            PlaceResults(result);
        }
    }
    private void GetSlotData()
    {
        SlotData = new CustomKeyValuePair<string, int>[]
        {
            new CustomKeyValuePair<string, int>("A,Wild,Bonus", 13),
            new CustomKeyValuePair<string, int>("Wild,Wild,Seven", 13),
            new CustomKeyValuePair<string, int>("Jackpot,Jackpot,A", 13),
            new CustomKeyValuePair<string, int>("Bonus,A,Jackpot", 13),
            new CustomKeyValuePair<string, int>("Wild,Bonus,A", 13),
            new CustomKeyValuePair<string, int>("A,A,A", 9),
            new CustomKeyValuePair<string, int>("Bonus,Bonus,Bonus", 8),
            new CustomKeyValuePair<string, int>("Seven,Seven,Seven", 7),
            new CustomKeyValuePair<string, int>("Wild,Wild,Wild", 6),
            new CustomKeyValuePair<string, int>("Jackpot,Jackpot,Jackpot", 5),
        };
    }

    private void PlaceResults(CustomKeyValuePair<string, int> result)
    {

        if (_perToIntervalDict.TryGetValue(result.Value, out (int, int)[] resultIntervals))
        {
            foreach (var interval in resultIntervals)
            {
                List<int> availableIndexes = GetAvailableIndexes(interval);
                if (availableIndexes.Count > 0)
                {
                    int randomIndex = availableIndexes[Random.Range(0, availableIndexes.Count)];
                    _results[randomIndex] = result;
                    _indexes[randomIndex] = -1;
                }
            }
        }
        else
        {
            Debug.LogError("Key not found!");
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
            //Debug.LogError("No available index between " + range.Item1 + " and " + range.Item2);
            Dictionary<int, CustomKeyValuePair<string, int>> _indexToResult = new Dictionary<int, CustomKeyValuePair<string, int>>();
            int heighestPer = GetHighestPer();
            for (int i = range.Item1; i <= range.Item2; i++)
            {
                if (_results[i].Value == heighestPer)
                {
                    _indexToResult.Add(i, _results[i]);
                }

            }

            var replace = _indexToResult.ElementAt(new System.Random().Next(_indexToResult.Count));
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
            if (heighestPer < item.Key) heighestPer = item.Key;
        }
        return heighestPer;
    }
    private void ReplaceIndex(CustomKeyValuePair<string, int> replacingElement)
    {
        List<int> emptyIndexes = new List<int>();

        foreach (var item in _indexes)
        {
            if (item != -1)
            {
                emptyIndexes.Add(item);
            }
        }
        int emptyty = Random.Range(0, emptyIndexes.Count);
        _results[emptyIndexes[emptyty]] = replacingElement;
        _indexes[emptyIndexes[emptyty]] = -1;
    }

    /// <param name="percentage">The percentage of the total spins for which to calculate intervals.</param>
    /// <param name="totalSpins">The total number of spins.</param>
    /// <returns>A list of tuples representing the start and end indices of each spin interval.</returns>
    /// <remarks>
    /// This method calculates spin intervals based on a given percentage of the total spins. It ensures that the intervals
    /// are as balanced as possible, even if the total number of spins is not evenly divisible by the percentage.
    /// </remarks>
    public (int, int)[] CalculateSpinIntervals(int percentage, int totalSpins = 100)
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
        //PrintIntervals(resultIntervals);
        return resultIntervals;
    }
}

[System.Serializable]
public class CustomKeyValuePair<K,V>
{
    public CustomKeyValuePair(K key, V value)
    {
        Key = key; Value = value;
    }
    public K Key;
    public V Value;
}
