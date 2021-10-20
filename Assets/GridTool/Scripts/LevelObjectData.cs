using JetBrains.Annotations;
using UnityEngine;

namespace GridTool.Scripts
{
    public struct LevelObjectData
    {
        public string DisplayName => string.IsNullOrEmpty(Name) ? "Empty" : Name;
        public string Name { get; set; }

        public LevelObjectData(string name)
        {
            Name = name;
        }
    }
}