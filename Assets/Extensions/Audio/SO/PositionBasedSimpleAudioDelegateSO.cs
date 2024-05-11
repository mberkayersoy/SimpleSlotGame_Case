using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.Audio
{
    [CreateAssetMenu(fileName = "PositionSimpleAudioDelegateSO", menuName = "Audio/Position Simple Audio Delegate")]
    public class PositionBasedSimpleAudioDelegateSO : SimpleAudioDelegateSO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"> In what position in the world space should it be played?</param>
        public override void Play(Vector3 position)
        {
            if (_clip == null)
                return;

            if (position == default)
            {
                Debug.LogWarning("If you want to play position based audio, don't forget the Vector3 position parameter!");
            }

            AudioPlayed?.Invoke(_clip, _volume, _pitch, position);
        }
    }
}
