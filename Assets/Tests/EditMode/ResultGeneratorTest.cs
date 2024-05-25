using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class ResultGeneratorTest
{
    private Dictionary<string, ResultData> _testResults;
    private int _totalSpin = 100;

    [SetUp]
    public void InitializeData()
    {
        SlotSymbolData[] testSymbols = new[]
        {
            new SlotSymbolData("Symbol1", 1, "path1", "path2"),
            new SlotSymbolData("Symbol2", 2, "path3", "path4"),
            new SlotSymbolData("Symbol3", 3, "path3", "path4")
        };

        _testResults = new Dictionary<string, ResultData>
        {
            { "Result1", new ResultData(13, testSymbols, "Result1", false, false) },
            { "Result2", new ResultData(13, testSymbols, "Result2", false, true) },
            { "Result3", new ResultData(13, testSymbols, "Result3", false, true) },
            { "Result4", new ResultData(13, testSymbols, "Result4", false, true) },
            { "Result5", new ResultData(13, testSymbols, "Result5", false, true) },
            { "Result6", new ResultData(9, testSymbols, "Result6", true, true) },
            { "Result7", new ResultData(8, testSymbols, "Result7", true, true) },
            { "Result8", new ResultData(7, testSymbols, "Result8", true, true) },
            { "Result9", new ResultData(6, testSymbols, "Result9", true, true) },
            { "Result10", new ResultData(5, testSymbols, "Result10", true, true) },
        };
    }

    [Test]
    public void AllIndexesUsed_Test()
    {
        ResultGenerator resultGenerator = new ResultGenerator(_testResults);
        var placedIndex = -1;
        for (int i = 0; i < resultGenerator.Indexes.Length; i++)
        {
            Assert.AreEqual(placedIndex, resultGenerator.Indexes[i]);
        }
    }

    [Test]
    public void AllResultsUsed_Test()
    {
        ResultGenerator resultGenerator = new ResultGenerator(_testResults);

        foreach (var result in _testResults.Values)
        {
            Assert.Contains(result, resultGenerator.AllSpinResults);
        }
    }

    [Test]
    public void AllSpinResultNotNull_Test()
    {
        ResultGenerator resultGenerator = new ResultGenerator(_testResults);

        Assert.IsFalse(resultGenerator.AllSpinResults.Any(result => result == null));
    }

    [Test]
    public void CalculateSpinIntervals_Length_Test()
    {
        var generator = new ResultGenerator(_testResults);
        var testCases = _testResults.Select(result => result.Value.ChancePer).ToArray();

        foreach (var percentage in testCases)
        {
            var actual = CalculateSpinIntervals(percentage, _totalSpin);

            Assert.That(actual.Length, Is.EqualTo(percentage));
        }
    }

    [Test]
    public void CalculateSpinIntervals_Sum_Test()
    {
        var generator = new ResultGenerator(_testResults);
        var testCases = _testResults.Select(result => result.Value.ChancePer).ToArray();

        foreach (var percentage in testCases)
        {
            var actual = CalculateSpinIntervals(percentage, _totalSpin);

            Assert.That(actual.Sum(interval => interval.Item2 - interval.Item1 + 1), Is.EqualTo(_totalSpin));
        }
    }

    [Test]
    public void CalculateSpinIntervals_Elements_Test()
    {
        var generator = new ResultGenerator(_testResults);
        var testCases = _testResults.Select(result => result.Value.ChancePer).ToArray();

        foreach (var percentage in testCases)
        {
            var expected = generator.CalculateSpinIntervals(percentage);
            var actual = CalculateSpinIntervals(percentage, _totalSpin);

            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }

    private (int, int)[] CalculateSpinIntervals(int percentage, int totalSpins)
    {
        (int, int)[] resultIntervals = new (int, int)[percentage];
        int spinsPerInterval = totalSpins / percentage;
        int remainingSpins = totalSpins % percentage;
        int start = 0;
        for (int i = 0; i < percentage; i++)
        {
            int end = start + spinsPerInterval - 1;
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
