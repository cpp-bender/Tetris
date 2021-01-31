using System;
using UnityEngine;

public class TetromonioController : MonoBehaviour,ITetromonioControllerService
{
    [SerializeField] GameObject centerPoint;
    private ISpawnControllerService spawnController;
    private IAudioControllerService audioControllerService;
    private GameManager gameManager;
    private bool canBeMoved = true;
    private const int lastHeight = 0;

    private void Awake()
    {
        spawnController = FindObjectOfType<SpawnController>();
        audioControllerService = FindObjectOfType<AudioController>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        Move();
    }
    private bool OnMove()
    {
        foreach (Transform block in centerPoint.transform)
        {
            if (block.position.x <= 0.5 || block.position.x >= 20.5 || Mathf.FloorToInt(block.position.y) <= 0)
            {
                return false;
            }
            if (GameManager.Grid[Mathf.FloorToInt(block.position.x - 0.5f), Mathf.FloorToInt(block.position.y - 1)] != null)
            {
                return false;
            }
        }
        return true;
    }
    public void Move()
    {
        if (canBeMoved && !GameManager.IsGameOver)
        {
            transform.position += new Vector3(0f, -GameManager.DropFactor, 0f);
            if (!OnMove())
            {
                transform.position -= new Vector3(0f, -GameManager.DropFactor, 0f);
                canBeMoved = false;
                SaveTetromonioToGrid();
                OnLastRowFull();
                spawnController.Spawn();
                GameManager.AddTetromonioToPool(transform);
                IsGameOver();
                return;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += Vector3.right;
                if (!OnMove())
                {
                    transform.position -= Vector3.right;
                }
            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left;
                if (!OnMove())
                {
                    transform.position -= Vector3.left;
                }
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                centerPoint.transform.eulerAngles -= new Vector3(0f, 0f, 90f);
                if (!OnMove())
                {
                    centerPoint.transform.eulerAngles += new Vector3(0f, 0f, 90f);
                }
            }
        }
    }
    private void SaveTetromonioToGrid()
    {
        foreach (Transform block in centerPoint.transform)
        {
            GameManager.Grid[Mathf.FloorToInt(block.position.x-0.5f), Mathf.FloorToInt(block.position.y-0.5f)]=block;
        }
    }
    private void OnLastRowFull()
    {
        for (int width = 0; width < GameManager.Width; width++)
        {
            if (GameManager.Grid[width, 0] == null)
            {
                return;
            }
        }
        audioControllerService.Play(name = "RowDeletedSound");
        ClearLastRow();
        ShiftEachRow();
    }
    public void ClearLastRow()
    {
        for (int width = 0; width < GameManager.Width; width++)
        {
            Destroy(GameManager.Grid[width, lastHeight].gameObject);
        }
    }
    public void ShiftEachRow()
    {
        for (int width = 0; width < GameManager.Width; width++)
        {
            for (int height = 0; height < GameManager.Height - 1; height++)
            {
                if (GameManager.Grid[width, height + 1] != null)
                {
                    GameManager.Grid[width, height] = GameManager.Grid[width, height + 1];
                    GameManager.Grid[width, height].gameObject.transform.position += new Vector3(0f, -1f, 0f);
                    GameManager.Grid[width, height + 1] = null;
                }
            }
        }
    }
    private void IsGameOver()
    {
        for (int x = 0; x < GameManager.Width; x++)
        {
            if (GameManager.Grid[x, 18] != null)
            {
                GameManager.IsGameOver = true;
                gameManager.OnGameOver();
                return;
            }
        }
    }
}