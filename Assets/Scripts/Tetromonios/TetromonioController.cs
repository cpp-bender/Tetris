using UnityEngine;

public class TetromonioController : MonoBehaviour
{
    [SerializeField] GameObject centerPoint;
    private SpawnController spawnController;
    private bool canBeMoved = true;
    private const int lastHeight = 0;

    private void Awake()
    {
        spawnController = FindObjectOfType<SpawnController>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (canBeMoved)
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
        ClearRow();
        ShiftRow();
    }
    void ClearRow()
    {
        for (int width = 0; width < GameManager.Width; width++)
        {
            Destroy(GameManager.Grid[width, lastHeight].gameObject);
        }
    }
    void ShiftRow()
    {
        for (int width = 0; width < GameManager.Width; width++)
        {
            for (int height = 0; height < GameManager.Height-1; height++)
            {
                if (GameManager.Grid[width,height+1]!=null)
                {
                    GameManager.Grid[width, height] = GameManager.Grid[width, height + 1];
                    GameManager.Grid[width, height].gameObject.transform.position += new Vector3(0f, -1f, 0f);
                    GameManager.Grid[width, height + 1] = null;
                }
            }
        }
    }
}