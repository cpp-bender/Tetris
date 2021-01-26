using UnityEngine;

public class TetromonioManager : MonoBehaviour
{
    [SerializeField] GameObject centerPoint;
    private SpawnManager spawnManager;
    private bool canBeMoved = true;

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        Move();
    }

    private void SaveTetromonioToGrid()
    {
        foreach (Transform block in centerPoint.transform)
        {
            GameManager.Grid[Mathf.FloorToInt(block.position.x), Mathf.FloorToInt(block.position.y)]=block;
        }
    }

    private bool OnMove()
    {
        foreach (Transform block in centerPoint.transform)
        {
            if (block.position.x<=0.5  || block.position.x>=19.5 || Mathf.FloorToInt(block.position.y)<=0.5)
            {
                return false;
            }
            if (GameManager.Grid[Mathf.FloorToInt(block.position.x), Mathf.FloorToInt(block.position.y)] != null)
            {
                return false;
            }
        }
        return true;
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
                spawnManager.Spawn();
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
}