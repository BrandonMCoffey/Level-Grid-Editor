#if UNITY_EDITOR
using GridTool.DataScripts;
using GridTool.DataScripts.GUI;
using UnityEditor;
using UnityEngine;

namespace Assets.GridTool.DataScripts.Editor
{
    [CustomEditor(typeof(ObjectData), true)]
    public class ObjectDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Separator();
            if (GUILayout.Button("Edit Object", GUILayout.Height(40))) {
                ObjectDesignerWindow.OpenWindow((ObjectData)target);
            }
        }
    }
}

#endif