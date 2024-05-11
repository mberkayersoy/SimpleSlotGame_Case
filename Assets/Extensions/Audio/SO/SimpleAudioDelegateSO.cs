using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.Audio
{
    [CreateAssetMenu(fileName = "SimpleAudioDelegateSO", menuName = "Audio/Simple Audio Delegate")]
    public class SimpleAudioDelegateSO : BaseAudioDelegateSO
    {
        [SerializeField] protected AudioClip _clip;
        [SerializeField, Range(0f,1f)] protected float _volume = 1;
        [SerializeField, Range(-3f, 3f)] protected float _pitch = 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"> Won't be use for SimpleAudioDelegateSO.</param>
        public override void Play(Vector3 position = default)
        {
            if (_clip == null)
                return;

            AudioPlayed?.Invoke(_clip, _volume, _pitch, default);
        }
    }
}
