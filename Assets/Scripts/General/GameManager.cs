using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameObject tetromonioPool;
    private static float dropFactor = 4f;
    private static int width = 20;
    private static int height = 20;
    private static Transform[,] grid = new Transform[width, height];
    private static bool isGameOver = false;
    private IAudioControllerService audioControllerService;

    public static float DropFactor { get { return dropFactor*Time.deltaTime; } }
    public static int Width { get { return width; } }
    public static int Height { get { return height; } }
    public static Transform[,] Grid { get { return grid; } }
    public static bool IsGameOver { get { return isGameOver; } set { isGameOver = value; } }

    private void Awake()
    {
        CreateTetromonioPool();
        audioControllerService = FindObjectOfType<AudioController>();
    }

    private void Start()
    {
        audioControllerService.Play(name = "MainTheme");
    }

    private  void CreateTetromonioPool()
    {
        tetromonioPool = new GameObject();
        tetromonioPool.name = "[Groups]";
    }
    public static void AddTetromonioToPool(Transform group)
    {
        group.parent = tetromonioPool.transform;
    }
    public  void OnGameOver()
    {
        FindObjectOfType<SpawnController>().gameObject.SetActive(false);
        audioControllerService.Stop(name = "MainTheme");
        audioControllerService.Play(name = "GameOverSound");
    }
}
