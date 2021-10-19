using UnityEngine;

namespace GridTool.Scripts
{
    [CreateAssetMenu]
    public class ObjectData : ScriptableObject
    {
        [Header("Tile Properties")]
        public int SortingPriority;
        public ObjectSpriteType ObjectType;
        [Header("Static Sprite")]
        public Sprite Static;
        [Header("Directional Sprites")]
        public Sprite Up;
        public Sprite Down;
        public Sprite Left;
        public Sprite Right;
        [Header("Blendable Sprites")]
        public bool BlendSprites = false;
        public Sprite TopLeft;
        public Sprite Top;
        public Sprite TopRight;
        public Sprite MiddleLeft;
        public Sprite Middle;
        public Sprite MiddleRight;
        public Sprite BottomLeft;
        public Sprite Bottom;
        public Sprite BottomRight;
    }
}