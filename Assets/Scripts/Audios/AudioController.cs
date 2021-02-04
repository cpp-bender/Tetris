using UnityEngine;
using System;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.General
{
    public class AudioController : MonoBehaviour, IAudioControllerService
    {
        [SerializeField] Sound[] sounds;

        private void Awake()
        {
            InitializeAudioSources();
        }
        private void InitializeAudioSources()
        {
            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.isOnLoop;
            }
        }
        public void Play(string audioName)
        {
            Sound soundToBePlayed = Array.Find(sounds, p => p.name == audioName);
            soundToBePlayed.source.Play();
        }
        public void Stop(string audioName)
        {
            Sound soundToBeStopped = Array.Find(sounds, p => p.name == audioName);
            soundToBeStopped.source.Stop();
        }
    }
}
