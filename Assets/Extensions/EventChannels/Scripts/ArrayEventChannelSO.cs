using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.EventChannels
{
    /// <summary>
    /// This event channel broadcasts and carries Boolean payload.
    /// </summary>
    [CreateAssetMenu(fileName = "ArrayEventChannelSO", menuName = "Events/ArrayEventChannelSO")]
    public class ArrayEventChannelSO<T> : GenericEventChannelSO<T[]>
    {

    }
}