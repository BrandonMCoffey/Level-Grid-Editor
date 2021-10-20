#if UNITY_EDITOR
using GridTool.Scripts;
using GridTool.Scripts.GUI;
using UnityEditor;
using UnityEngine;

namespace Assets.GridTool.Scripts.Editor
{
    [CustomEditor(typeof(LevelData), true)]
    public class LevelDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LevelData data = (LevelData)target;

            EditorGUILayout.Separator();
            GUILayout.BeginHorizontal();
            for (int w = data.Level.GetLength(0) - 1; w >= 0; w--) {
                GUILayout.BeginVertical();
                for (int h = 0; h < data.Level.GetLength(1); h++) {
                    string value = data.Level[w, h];
                    GUILayout.Label(string.IsNullOrEmpty(value) ? "Empty" : value, GUILayout.MinWidth(0));
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.Separator();

            EditorGUILayout.Separator();
            if (GUILayout.Button("Edit Object", GUILayout.Height(40))) {
                LevelDesignerWindow.OpenWindow(data);
            }
        }
    }
}

#endif