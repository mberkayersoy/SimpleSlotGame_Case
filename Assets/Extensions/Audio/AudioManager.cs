using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private BaseAudioDelegateSO[] _audioClips;
        private AudioSource _audioSource;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            foreach (BaseAudioDelegateSO audioDelegateSO in _audioClips)
            {
                audioDelegateSO.AudioPlayed += PlayAudio;
            }
        }

        private void PlayAudio(AudioClip clip, float volume, float pitch, Vector3 position)
        {
            if (position == default)
            {
                _audioSource.PlayOneShot(clip, volume);
                _audioSource.pitch = pitch;
            }
            else
            {
                AudioSource.PlayClipAtPoint(clip, position, volume);
            }
        }

        private void OnDestroy()
        {
            foreach (BaseAudioDelegateSO audioDelegateSO in _audioClips)
            {
                audioDelegateSO.AudioPlayed -= PlayAudio;
            }
        }
    }
}
