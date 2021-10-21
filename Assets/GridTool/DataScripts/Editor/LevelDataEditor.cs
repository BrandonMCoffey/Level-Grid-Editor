#if UNITY_EDITOR
using GridTool.DataScripts;
using GridTool.DataScripts.GUI;
using UnityEditor;
using UnityEngine;

namespace Assets.GridTool.DataScripts.Editor
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
            if (GUILayout.Button("Save Changes from String", GUILayout.Height(20))) {
                data.ReadFromString();
            }
            if (GUILayout.Button("Reset String", GUILayout.Height(20))) {
                data.SaveLevel();
            }
            if (GUILayout.Button("Export to CSV", GUILayout.Height(20))) {
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            GUILayout.BeginHorizontal();
            data.CheckValid();
            for (int w = 0; w < data.Width; w++) {
                GUILayout.BeginVertical();
                for (int h = 0; h < data.Height; h++) {
                    GUILayout.Label(data.Get(w, h).DisplayName, GUILayout.MinWidth(0));
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