using Assets.Scripts.General;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tetromonios
{
    public class TetromonioController : MonoBehaviour, ITetromonioController
    {
        [SerializeField] GameObject centerPoint;
        [SerializeField] private LevelInfo level;
        private ISpawnController spawnController;
        private IAudioController audioController;
        private bool canBeMoved = true;

        private void Awake()
        {
            spawnController = FindObjectOfType<SpawnController>();
            audioController = FindObjectOfType<AudioController>();
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
                if (level.Grid[Mathf.FloorToInt(block.position.x - 0.5f), Mathf.FloorToInt(block.position.y - 1)] != null)
                {
                    return false;
                }
            }
            return true;
        }
        public void Move()
        {
            if (canBeMoved && !level.IsGameOver)
            {
                transform.position += new Vector3(0f, -level.DropFactor, 0f);
                if (!CanBeMoved())
                {
                    transform.position -= new Vector3(0f, -level.DropFactor, 0f);
                    canBeMoved = false;
                    SaveTetromonioToGrid();
                    CheckIfLastRowFull();
                    GameManager.AddTetromonioToPool(transform);
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
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.position += new Vector3(0f, -level.DropFactor * 2f, 0f);
                    if (!canBeMoved)
                    {
                        transform.position -= new Vector3(0f, -level.DropFactor * 2f, 0f);
                    }
                }
            }
        }
        private void SaveTetromonioToGrid()
        {
            foreach (Transform block in centerPoint.transform)
            {
                level.Grid[Mathf.FloorToInt(block.position.x - 0.5f), Mathf.FloorToInt(block.position.y - 0.5f)] = block;
            }
        }
        private void CheckIfLastRowFull()
        {
            for (int width = 0; width < level.Width; width++)
            {
                if (level.Grid[width, 0] == null)
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
            for (int width = 0; width < level.Width; width++)
            {
                Destroy(level.Grid[width, level.LastRowHeight].gameObject);
            }
        }
        public void ShiftEachRow()
        {
            for (int width = 0; width < level.Width; width++)
            {
                for (int height = 0; height < level.Height - 1; height++)
                {
                    if (level.Grid[width, height + 1] != null)
                    {
                        level.Grid[width, height] = level.Grid[width, height + 1];
                        level.Grid[width, height].gameObject.transform.position += new Vector3(0f, -1f, 0f);
                        level.Grid[width, height + 1] = null;
                    }
                }
            }
        }
    }
}