using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.EventChannels
{
    /// <summary>
    /// This event channel broadcasts and carries Boolean payload.
    /// </summary>
    [CreateAssetMenu(fileName = "ListEventChannelSO", menuName = "Events/ListEventChannelSO")]
    public class ListEventChannelSO<T> : GenericEventChannelSO<List<T>>
    {

    }
}