using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class GameManager : MonoBehaviour
    {
        private static GameObject tetromonioPool;
        private IAudioController audioController;

        public event System.Action OnGameOver;

        private void Awake()
        {
            CreateTetromonioPool();
            audioController = FindObjectOfType<AudioController>();
            OnGameOver += GameOver;
        }

        private void Start()
        {
            audioController.Play(GameSound.MainTheme);
        }

        private void Update()
        {
            if (!LevelInfo.IsGameOver)
            {
                CheckIfGameEnded();
            }
        }

        private void GameOver()
        {
            audioController.Stop(GameSound.MainTheme);
            audioController.Play(GameSound.GameOverSound);
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
        private void CheckIfGameEnded()
        {
            for (int x = 0; x < LevelInfo.Width; x++)
            {
                if (LevelInfo.Grid[x, Tetromonio.FirstRowHeight] != null)
                {
                    LevelInfo.IsGameOver = true;
                    OnGameOver?.Invoke();
                    return;
                }
            }
        }
    }
}