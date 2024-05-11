using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyExtensions.Audio
{
    /// <summary>
    /// Useful for walking,running etc. type audios.
    /// </summary>
    [CreateAssetMenu(fileName = "RandomAudioDelegateSO", menuName = "Audio/Random Audio Delegate")]
    public class RandomAudioDelegateSO : BaseAudioDelegateSO
    {
        [SerializeField] protected AudioClip[] _clips;
        [SerializeField] protected RangedFloat _volumeRange;
        [SerializeField] protected RangedFloat _pitchRange;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"> Won't be use for RandomAudioDelegateSO.</param>
        public override void Play(Vector3 position = default)
        {
            if (_clips.Length < 0)
                return;
            
            AudioClip randomAudio = _clips[Random.Range(0, _clips.Length)];
            float volume = Random.Range(_volumeRange.minValue, _volumeRange.maxValue);
            float pitch = Random.Range(_pitchRange.minValue, _pitchRange.maxValue);

            AudioPlayed?.Invoke(randomAudio, volume, pitch, default);
        }
    }

    [Serializable]
    public struct RangedFloat
    {
        public float minValue;
        public float maxValue;
    }

}
