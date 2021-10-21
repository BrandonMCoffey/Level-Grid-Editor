#if UNITY_EDITOR
using GridTool.GameScripts;
using UnityEditor;
using UnityEngine;

namespace Assets.GridTool.GameScripts.Editor
{
    [CustomEditor(typeof(LevelLoader), true)]
    public class LevelLoaderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Separator();
            if (Application.isPlaying) {
                if (GUILayout.Button("Load Level", GUILayout.Height(40))) {
                    ((LevelLoader)target).Load();
                }
            }
        }
    }
}

#endif