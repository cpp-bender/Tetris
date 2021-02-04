using UnityEngine;

namespace Assets.Scripts.General
{
    class Scene:MonoBehaviour
    {
        private static float dropFactor = 2f;
        private static int width = 20;
        private static int height = 20;
        private static bool isGameOver = false;
        private static Transform[,] grid=new Transform[width,height];

        public static float DropFactor { get =>dropFactor*Time.deltaTime;}
        public static int Width  { get=>width;}
        public static int Height { get => height; }
        public static bool IsGameOver { get=>isGameOver; set=>isGameOver=value; }
        public static Transform[,] Grid { get=>grid; set=>grid=value; }
    }
}
