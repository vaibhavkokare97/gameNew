using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class AudioData : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip matchSound, turnSound, flipSound;

        private void OnEnable()
        {
            AudioManager.OnPlayOneShot += HandlePlayOneShot;
        }

        private void HandlePlayOneShot(int obj)
        {
            PlayOneShot(obj switch
            {
                0 => matchSound,
                1 => turnSound,
                2 => flipSound,
                _ => null
            });
        }

        public void PlayOneShot(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        private void OnDisable()
        {
            AudioManager.OnPlayOneShot -= HandlePlayOneShot;
        }
    }
}