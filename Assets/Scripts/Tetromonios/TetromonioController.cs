using Assets.Scripts.General;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tetromonios
{
    public class TetromonioController : MonoBehaviour, ITetromonioControllerService
    {
        [SerializeField] GameObject centerPoint;
        private ISpawnControllerService spawnController;
        private IAudioControllerService audioControllerService;
        private GameManager gameManager;
        private const int LastHeight = 0;
        private const int FirstHeight = 18;
        private bool canBeMoved = true;

        private void Awake()
        {
            spawnController = FindObjectOfType<SpawnController>();
            audioControllerService = FindObjectOfType<AudioController>();
            gameManager = FindObjectOfType<GameManager>();
        }
        private void Start()
        {
            GameManager.AddTetromonioToPool(transform);
        }
        void Update()
        {
            Move();
        }
        private bool CanBeMoved()
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
                if (!CanBeMoved())
                {
                    transform.position -= new Vector3(0f, -GameManager.DropFactor, 0f);
                    canBeMoved = false;
                    SaveTetromonioToGrid();
                    CheckIfLastRowFull();
                    GameManager.AddTetromonioToPool(transform);
                    CheckIfGameEnded();
                    spawnController.Spawn();
                    return;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.position += Vector3.right;
                    if (!CanBeMoved())
                    {
                        transform.position -= Vector3.right;
                    }
                }

                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.position += Vector3.left;
                    if (!CanBeMoved())
                    {
                        transform.position -= Vector3.left;
                    }
                }

                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    centerPoint.transform.eulerAngles -= new Vector3(0f, 0f, 90f);
                    if (!CanBeMoved())
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
                GameManager.Grid[Mathf.FloorToInt(block.position.x - 0.5f), Mathf.FloorToInt(block.position.y - 0.5f)] = block;
            }
        }
        private void CheckIfLastRowFull()
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
                Destroy(GameManager.Grid[width, LastHeight].gameObject);
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
        private void CheckIfGameEnded()
        {
            for (int x = 0; x < GameManager.Width; x++)
            {
                if (GameManager.Grid[x, FirstHeight] != null)
                {
                    GameManager.IsGameOver = true;
                    gameManager.OnGameOver();
                    return;
                }
            }
        }
    }
}