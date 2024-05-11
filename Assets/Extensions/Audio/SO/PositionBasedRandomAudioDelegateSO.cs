using UnityEngine;

namespace MyExtensions.Audio
{
    [CreateAssetMenu(fileName = "PositionRandomAudioDelegateSO", menuName = "Audio/Position Random Audio Delegate")]
    public class PositionBasedRandomAudioDelegateSO : RandomAudioDelegateSO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"> In what position in the world space should it be played?</param>
        public override void Play(Vector3 position)
        {
            if (_clips.Length < 0)
                return;

            AudioClip randomAudio = _clips[Random.Range(0, _clips.Length)];
            float volume = Random.Range(_volumeRange.minValue, _volumeRange.maxValue);
            float pitch = Random.Range(_pitchRange.minValue, _pitchRange.maxValue);

            if (position == default)
            {
                Debug.LogWarning("If you want to play position based audio, don't forget the Vector3 position parameter!");
            }
            AudioPlayed?.Invoke(randomAudio, volume, pitch, position);
        }
    }
}
