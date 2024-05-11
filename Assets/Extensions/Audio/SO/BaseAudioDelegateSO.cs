using System;
using UnityEngine;

namespace MyExtensions.Audio
{
    /// <summary>
    /// An example of using a ScriptableObject-based delegate. This
    /// encapsulates some methods to play back a sound.
    /// </summary>
    public abstract class BaseAudioDelegateSO : ScriptableObject
    {
        public Action<AudioClip, float, float, Vector3> AudioPlayed;
        public abstract void Play(Vector3 position = default);
    }
}
