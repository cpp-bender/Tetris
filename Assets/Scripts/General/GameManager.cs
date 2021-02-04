using Assets.Scripts.Interfaces;
using Assets.Scripts.Tetromonios;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class GameManager : MonoBehaviour
    {
        private static GameObject tetromonioPool;

        private IAudioControllerService audioControllerService;

        private void Awake()
        {
            CreateTetromonioPool();
            audioControllerService = FindObjectOfType<AudioController>();
        }
        private void Start()
        {
            audioControllerService.Play(name = "MainTheme");
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
        public void OnGameOver()
        {
            FindObjectOfType<SpawnController>().gameObject.SetActive(false);
            audioControllerService.Stop(name = "MainTheme");
            audioControllerService.Play(name = "GameOverSound");
        }
    }
}
