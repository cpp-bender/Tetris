using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelInfo level;
        private static GameObject tetromonioPool;
        private IAudioController audioController;

        public event System.Action OnGameOver;

        private void Awake()
        {
            CreateTetromonioPool();
            audioController = FindObjectOfType<AudioController>();
            OnGameOver += GameOver;
            level.InitializeLevelData();
        }

        private void Start()
        {
            audioController.Play(GameSound.MainTheme);
        }

        private void Update()
        {
            if (!level.IsGameOver)
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
            for (int x = 0; x < level.Width; x++)
            {
                if (level.Grid[x, level.FirstRowHeight] != null)
                {
                    level.IsGameOver = true;
                    OnGameOver?.Invoke();
                    return;
                }
            }
        }
    }
}