using MyExtensions.EventChannels;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystemsCookbook
{
    /// <summary>
    /// General Event Channel that broadcasts and carries Vector3 payload.
    /// </summary>
    /// 
    [CreateAssetMenu(menuName = "Events/Vector3 Event Channel", fileName = "Vector3EventChannel")]
    public class Vector3EventChannelSO : GenericEventChannelSO<Vector3>
    {

    }
}