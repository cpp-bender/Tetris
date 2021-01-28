using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] tetromonios;

    private void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {
        Instantiate(tetromonios[Random.Range(0, tetromonios.Length)], transform.position, Quaternion.identity);
    }
}
