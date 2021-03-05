using UnityEngine;

namespace Assets.Scripts.General
{
    [CreateAssetMenu(menuName = "Tetris/Level/Level Data")]
    class LevelInfo : ScriptableObject
    {
        [SerializeField] private float dropSpeed;

        private bool isGameOver;
        private int width;
        private int height;
        private int lastRowHeight;
        private int firstRowHeight;
        private Transform[,] grid;

        public void InitializeLevelData()
        {
            width = 20;
            height = 20;
            lastRowHeight = 0;
            firstRowHeight = 18;
            isGameOver = false;
            grid = new Transform[Width, Height];
        }

        public float DropFactor { get => dropSpeed; }
        public int Width { get => width; }
        public int Height { get => height; }
        public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
        public Transform[,] Grid { get => grid; set => grid = value; }
        public int LastRowHeight { get => lastRowHeight; }
        public int FirstRowHeight { get => firstRowHeight; }
    }
}
