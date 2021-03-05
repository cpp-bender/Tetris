using UnityEngine;

namespace Assets.Scripts.General
{
    public enum GameSound
    {
        MainTheme,
        RowDeletedSound,
        GameOverSound
    }
    [System.Serializable]
    public class Sound
    {
        [SerializeField]
        private GameSound gameSound;

        [SerializeField]
        private string name;

        [SerializeField]
        private AudioClip clip;

        [SerializeField]
        [Range(0f, 1f)]
        private float volume;

        [SerializeField]
        private bool isOnLoop;

        [HideInInspector]
        public AudioSource source;

        public string Name { get => name; set => name = value; }
        public AudioClip Clip { get => clip; set => clip = value; }
        public bool IsOnLoop { get => isOnLoop; set => isOnLoop = value; }
        public float Volume { get => volume; set => volume = value; }
        public GameSound GameSound { get => gameSound; set => gameSound = value; }
    }
}