using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.General
{
    public class AudioController : MonoBehaviour, IAudioController
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
                sound.source.clip = sound.Clip;
                sound.source.volume = sound.Volume;
                sound.source.loop = sound.IsOnLoop;
            }
        }
        public void Play(GameSound gameSound)
        {
            Sound soundToBePlayed =System.Array.Find(sounds, p => p.GameSound == gameSound);
            soundToBePlayed.source.Play();
        }
        public void Stop(GameSound gameSound)
        {
            Sound soundToBeStopped =System.Array.Find(sounds, p => p.GameSound == gameSound);
            soundToBeStopped.source.Stop();
        }
    }
}
