using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.RandomSelection
{
    public interface IRandomSelectedWithWeight
    {
        public float Weight { get; }
    }

    public class RandomSelectorWithWeight<T> where T : IRandomSelectedWithWeight
    {
        public T GetRandomWithWeight(T[] weightedValues)
        {
            T selected = default;
            var totalWeight = 0f;
            foreach (var item in weightedValues)
            {
                totalWeight += item.Weight;
            }

            var randomWeightValue = Random.Range(0, totalWeight);
            var processeWeight = 0f;
            foreach (var item in weightedValues)
            {
                processeWeight += item.Weight;

                if (randomWeightValue <= processeWeight)
                {
                    selected = item;
                    break;
                }
            }
            return selected;
        }
    }

}
