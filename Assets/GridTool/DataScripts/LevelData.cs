using System;
using UnityEngine;

namespace GridTool.DataScripts
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        [Header("Level Properties")]
        public string Name = "";
        public int Width = 10;
        public int Height = 5;

        [Header("Level")]
        public ObjectCollection Collection;
        [TextArea] public string LevelString = "";
        public LevelObjectData[,] Level;

        public LevelObjectData Get(int w, int h)
        {
            return Level[w, h];
        }

        public void CheckValid()
        {
            // If level does not exist
            if (Level == null) {
                if (!string.IsNullOrEmpty(LevelString)) {
                    // Read level string
                    try {
                        var arr = LevelString.Split('\n');
                        var rowArr = arr[0].Split(',');
                        int width = int.Parse(rowArr[0]);
                        int height = int.Parse(rowArr[1]);

                        Level = new LevelObjectData[width, height];
                        for (int y = 0; y < height; y++) {
                            rowArr = arr[y + 1].Split(',');
                            for (int x = 0; x < width; x++) {
                                Level[x, y] = new LevelObjectData(rowArr[x]);
                            }
                        }
                        return;
                    } catch {
                        Console.WriteLine("Invalid level string");
                        LevelString = "";
                    }
                }
                // Create empty level
                Level = new LevelObjectData[Width, Height];
                for (int x = 0; x < Width; x++) {
                    for (int y = 0; y < Height; y++) {
                        Level[x, y] = new LevelObjectData();
                    }
                }
                SaveLevel();
                return;
            }
            // Check for resizing level
            if (Level.GetLength(0) != Width || Level.GetLength(1) != Height) {
                LevelObjectData[,] newArr = new LevelObjectData[Width, Height];
                for (int x = 0; x < Width; x++) {
                    for (int y = 0; y < Height; y++) {
                        if (x < Level.GetLength(0) && y < Level.GetLength(1)) {
                            newArr[x, y] = Level[x, y];
                        } else {
                            newArr[x, y] = new LevelObjectData();
                        }
                    }
                }
                Level = newArr;
            }
        }

        public void CheckValid(int width, int height)
        {
            if (Width == width && Height == height) return;
            Width = width;
            Height = height;
            CheckValid();
        }

        public void SaveLevel()
        {
            if (Level == null) {
                Debug.Log("Error: Invalid Level");
                return;
            }
            string levelDataString = Width + "," + Height + "\n";

            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    levelDataString += Level[x, y].Name + (x == Width - 1 ? "" : ",");
                }
                levelDataString += "\n";
            }
            LevelString = levelDataString;
        }
    }
}