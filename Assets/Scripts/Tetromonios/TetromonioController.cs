using Assets.Scripts.General;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tetromonios
{
    public class TetromonioController : MonoBehaviour, ITetromonioController
    {
        [SerializeField] GameObject centerPoint;
        private ISpawnController spawnController;
        private IAudioController audioController;
        private GameManager gameManager;
        private bool canBeMoved = true;

        private void Awake()
        {
            spawnController = FindObjectOfType<SpawnController>();
            audioController = FindObjectOfType<AudioController>();
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
                if (LevelInfo.Grid[Mathf.FloorToInt(block.position.x - 0.5f), Mathf.FloorToInt(block.position.y - 1)] != null)
                {
                    return false;
                }
            }
            return true;
        }
        public void Move()
        {
            if (canBeMoved && !LevelInfo.IsGameOver)
            {
                transform.position += new Vector3(0f, -LevelInfo.DropFactor, 0f);
                if (!CanBeMoved())
                {
                    transform.position -= new Vector3(0f, -LevelInfo.DropFactor, 0f);
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
                LevelInfo.Grid[Mathf.FloorToInt(block.position.x - 0.5f), Mathf.FloorToInt(block.position.y - 0.5f)] = block;
            }
        }
        private void CheckIfLastRowFull()
        {
            for (int width = 0; width < LevelInfo.Width; width++)
            {
                if (LevelInfo.Grid[width, 0] == null)
                {
                    return;
                }
            }
            audioController.Play(GameSound.RowDeletedSound);
            ClearLastRow();
            ShiftEachRow();
        }
        public void ClearLastRow()
        {
            for (int width = 0; width < LevelInfo.Width; width++)
            {
                Destroy(LevelInfo.Grid[width, Tetromonio.LastRowHeight].gameObject);
            }
        }
        public void ShiftEachRow()
        {
            for (int width = 0; width < LevelInfo.Width; width++)
            {
                for (int height = 0; height < LevelInfo.Height - 1; height++)
                {
                    if (LevelInfo.Grid[width, height + 1] != null)
                    {
                        LevelInfo.Grid[width, height] = LevelInfo.Grid[width, height + 1];
                        LevelInfo.Grid[width, height].gameObject.transform.position += new Vector3(0f, -1f, 0f);
                        LevelInfo.Grid[width, height + 1] = null;
                    }
                }
            }
        }
        private void CheckIfGameEnded()
        {
            for (int x = 0; x < LevelInfo.Width; x++)
            {
                if (LevelInfo.Grid[x, Tetromonio.FirstRowHeight] != null)
                {
                    LevelInfo.IsGameOver = true;
                    gameManager.GameOver();
                    return;
                }
            }
        }
    }
}