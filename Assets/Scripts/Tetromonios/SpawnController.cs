using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tetromonios
{
    public class SpawnController : MonoBehaviour, ISpawnController
    {
        [SerializeField] GameObject[] tetromonios;

        private void Awake()
        {
            Spawn();
        }
        public void Spawn()
        {
            int index = Random.Range(0, tetromonios.Length);
            Instantiate(tetromonios[index], transform.position, Quaternion.identity);
        }
    }
}
