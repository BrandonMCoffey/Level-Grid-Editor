using UnityEngine;

namespace GridTool.Scripts
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        [Header("Level Properties")]
        public string Name = "";
        public int Width = 10;
        public int Height = 5;

        [Header("Level")]
        public string[,] Level = new string[10, 5];
    }
}