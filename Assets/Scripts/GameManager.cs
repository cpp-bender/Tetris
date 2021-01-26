using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Discrete Fields
    private static float fallFactor = 0.017f;
    private static int width = 20;
    private static int height = 20;
    private static Transform[,] grid = new Transform[width, height];
    #endregion

    #region Abstract Fields
    public static GameManager Instance { get; private set; }
    public static float FallFactor { get { return fallFactor; }}
    public static int Width { get { return width; }}
    public static int Height { get { return height; } }
    public static Transform[,] Grid { get { return grid; } }
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
