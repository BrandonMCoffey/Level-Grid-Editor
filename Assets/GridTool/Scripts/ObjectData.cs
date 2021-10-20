using UnityEngine;

namespace GridTool.Scripts
{
    [CreateAssetMenu]
    public class ObjectData : ScriptableObject
    {
        [Header("Object Properties")]
        public string Name;
        public int SortingPriority;
        public Color MixColor = Color.white;

        [Header("Sprite Properties")]
        public ObjectSpriteType SpriteType;
        public int SpriteAnimationFrames = 1;

        [Header("Sprites")]
        public Sprite[] Static = { null };
        public Sprite[] Up = { null };
        public Sprite[] Down = { null };
        public Sprite[] Left = { null };
        // Sprite right == Static Sprite
    }
}