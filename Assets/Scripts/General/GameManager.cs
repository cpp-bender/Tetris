using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameObject tetromonioPool;
    private static float dropFactor = 4f;
    private static int width = 20;
    private static int height = 20;
    private static Transform[,] grid = new Transform[width, height];
    public static float DropFactor { get { return dropFactor*Time.deltaTime; } }
    public static int Width { get { return width; } }
    public static int Height { get { return height; } }
    public static Transform[,] Grid { get { return grid; } }

    private void Awake()
    {
        CreateTetromonioPool();
    }

    private void Start()
    {
        FindObjectOfType<AudioController>().Play(name = "MainTheme");
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
}
