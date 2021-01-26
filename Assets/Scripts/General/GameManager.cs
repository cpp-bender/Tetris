using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameObject tetromonioPool;
    private static float dropFactor = 0.1f;
    private static int width = 20;
    private static int height = 20;
    private static Transform[,] grid = new Transform[width, height];

    public static GameManager Instance { get; private set; }
    public static float DropFactor { get { return dropFactor; } }
    public static int Width { get { return width; } }
    public static int Height { get { return height; } }
    public static Transform[,] Grid { get { return grid; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CreateTetromonioPool();
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(tetromonioPool);
        }
        else
        {
            Destroy(this.gameObject);
            Destroy(tetromonioPool);
        }
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
