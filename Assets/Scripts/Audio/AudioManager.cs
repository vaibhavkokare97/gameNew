using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class AudioManager
    {
        public AudioManager()
        {
            
        }

        public static Action<int> OnPlayOneShot;

        public void PlayOneShot(int clipId)
        {
            OnPlayOneShot?.Invoke(clipId);
        }

    }
}