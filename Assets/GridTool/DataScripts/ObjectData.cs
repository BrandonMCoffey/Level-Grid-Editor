using UnityEngine;

namespace GridTool.DataScripts
{
    [CreateAssetMenu]
    public class ObjectData : ScriptableObject
    {
        [Header("Object Properties")]
        public string Name = "";
        public int SortingPriority = 1;
        public Color MixColor = Color.white;

        [Header("Sprite Properties")]
        public ObjectSpriteType SpriteType;
        public int SpriteAnimationFrames = 1;

        [Header("Sprites")]
        public Texture2D Texture;
        public Sprite[] Static = { null };
        public Sprite[] Up = { null };
        public Sprite[] Down = { null };
        public Sprite[] Left = { null };
        // Sprite right == Static Sprite
    }

    public enum ObjectSpriteType
    {
        Static,
        Directional,
    }
}