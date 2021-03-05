using Assets.Scripts.Interfaces;
using Assets.Scripts.Tetromonios;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class GameManager : MonoBehaviour
    {
        private static GameObject tetromonioPool;
        private IAudioController audioController;
        private SpawnController spawnController;

        private void Awake()
        {
            CreateTetromonioPool();
            audioController = FindObjectOfType<AudioController>();
            spawnController = FindObjectOfType<SpawnController>();
        }
        private void Start()
        {
            audioController.Play(GameSound.MainTheme);
        }
        private void CreateTetromonioPool()
        {
            tetromonioPool = new GameObject();
            tetromonioPool.name = "[Groups]";
        }
        public static void AddTetromonioToPool(Transform group)
        {
            group.parent = tetromonioPool.transform;
        }
        public void GameOver()
        {
            spawnController.gameObject.SetActive(false);
            audioController.Stop(GameSound.MainTheme);
            audioController.Play(GameSound.GameOverSound);
        }
    }
}
