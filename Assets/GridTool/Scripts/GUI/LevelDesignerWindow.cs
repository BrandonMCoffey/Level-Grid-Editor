#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace GridTool.Scripts.GUI
{
    public class LevelDesignerWindow : EditorWindow
    {
        private Rect _headerSection;
        private Rect _loadSection;
        private Rect _mainSection;
        private Rect _objectSection;

        private Texture2D _headerSectionTexture;
        private Texture2D _mainSectionTexture;
        private Color _headerSectionColor = new Color(0.5f, 0.5f, 0.5f);
        private Color _mainSectionColor = new Color(0.2f, 0.2f, 0.2f);

        private static LevelData _levelData;
        private static LevelData _loadLevelData;
        private static LevelData _overrideData;

        private static string _lastPath = "";

        private static int _maxWidth = 20;
        private static int _maxHeight = 16;

        #region Initialization

        [MenuItem("Window/Level Designer")]
        private static void OpenWindow()
        {
            LevelDesignerWindow window = (LevelDesignerWindow)GetWindow(typeof(LevelDesignerWindow), false, "Level Designer");
            window.minSize = new Vector2(800, 500);
            window.Show();
        }

        public static void OpenWindow(LevelData data)
        {
            LevelDesignerWindow window = (LevelDesignerWindow)GetWindow(typeof(LevelDesignerWindow), false, "Level Designer");
            window.minSize = new Vector2(800, 500);
            _levelData = ScriptableObject.Instantiate(data);
            _overrideData = data;
            window.Show();
        }

        private void OnEnable()
        {
            InitTextures();
            InitData();
        }

        // Initializes Texture 2D values
        private void InitTextures()
        {
            _headerSectionTexture = new Texture2D(1, 1);
            _headerSectionTexture.SetPixel(0, 0, _headerSectionColor);
            _headerSectionTexture.Apply();

            _mainSectionTexture = new Texture2D(1, 1);
            _mainSectionTexture.SetPixel(0, 0, _mainSectionColor);
            _mainSectionTexture.Apply();
        }

        private void InitData()
        {
            if (_levelData == null) {
                _levelData = (LevelData)ScriptableObject.CreateInstance(typeof(LevelData));
                _overrideData = null;
            }
        }

        #endregion

        #region Setup

        // Called once or more during interactions with user
        private void OnGUI()
        {
            DrawLayouts();
            DrawHeader();
            DrawLoad();
            DrawMain();
            DrawObjectSection();
        }

        // Defines rect values and paints textures
        private void DrawLayouts()
        {
            int headerHeight = 60;
            int loadWidth = 160;
            int halfPadding = 4;
            int separator = 3 * Screen.width / 4;

            Rect headerFill = new Rect(0, 0, Screen.width, headerHeight);

            _headerSection.x = halfPadding * 2;
            _headerSection.y = halfPadding * 2;
            _headerSection.width = Screen.width - loadWidth - halfPadding * 3;
            _headerSection.height = headerHeight - halfPadding * 4;

            _loadSection.x = Screen.width - loadWidth + halfPadding;
            _loadSection.y = halfPadding * 2;
            _loadSection.width = loadWidth - halfPadding * 3;
            _loadSection.height = headerHeight - halfPadding * 4;

            _mainSection.x = halfPadding * 2;
            _mainSection.y = headerHeight + halfPadding * 2;
            _mainSection.width = separator - halfPadding * 3;
            _mainSection.height = Screen.height - headerHeight - halfPadding * 8;

            _objectSection.x = separator + halfPadding;
            _objectSection.y = headerHeight + halfPadding * 2;
            _objectSection.width = Screen.width - separator - halfPadding * 3;
            _objectSection.height = Screen.height - headerHeight - halfPadding * 8;

            UnityEngine.GUI.DrawTexture(headerFill, _headerSectionTexture);
            UnityEngine.GUI.DrawTexture(_mainSection, _mainSectionTexture);
            UnityEngine.GUI.DrawTexture(_objectSection, _mainSectionTexture);
        }

        #endregion

        #region Header

        private void DrawHeader()
        {
            GUILayout.BeginArea(_headerSection);
            GUIStyle titleStyle = new GUIStyle { fontSize = 25, stretchHeight = true, alignment = TextAnchor.MiddleLeft };
            GUILayout.Label(" Level Designer", titleStyle);
            GUILayout.EndArea();
        }

        private void DrawLoad()
        {
            GUILayout.BeginArea(_loadSection);
            EditorGUILayout.BeginVertical();
            _loadLevelData = (LevelData)EditorGUILayout.ObjectField(_loadLevelData, typeof(LevelData), false);
            if (GUILayout.Button("Load Existing")) {
                LoadExistingAsset();
            }
            EditorGUILayout.EndVertical();
            GUILayout.EndArea();
        }

        private void LoadExistingAsset()
        {
            if (_loadLevelData != null) {
                _levelData = ScriptableObject.Instantiate(_loadLevelData);
                _overrideData = _loadLevelData;
                _loadLevelData = null;
            } else {
                //EditorUtility.OpenFilePanel("Select Existing Object", "", "asset");
            }
        }

        #endregion

        #region Level Map

        private void DrawMain()
        {
            GUILayout.BeginArea(_mainSection);

            GUILayout.BeginHorizontal();
            for (int x = 0; x < _levelData.Level.GetLength(0); x++) {
                GUILayout.BeginVertical();
                for (int y = 0; y < _levelData.Level.GetLength(1); y++) {
                    _levelData.Level[x, y] = GUILayout.TextField(_levelData.Level[x, y], GUILayout.MinWidth(0));
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        #endregion

        #region Settings

        private void DrawObjectSection()
        {
            GUILayout.BeginArea(_objectSection);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Name:");
            _levelData.Name = EditorGUILayout.TextField(_levelData.Name);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Width:");
            int w = EditorGUILayout.IntField(_levelData.Width, GUILayout.MinWidth(0));
            GUILayout.Label("Height:");
            int h = EditorGUILayout.IntField(_levelData.Height, GUILayout.MinWidth(0));
            EditorGUILayout.EndHorizontal();

            w = Mathf.Clamp(w, 1, _maxWidth);
            h = Mathf.Clamp(h, 1, _maxHeight);
            if (w != _levelData.Width || h != _levelData.Height) {
                _levelData.Width = w;
                _levelData.Height = h;
                string[,] newArr = new string[w, h];
                for (int x = 0; x < w; x++) {
                    for (int y = 0; y < h; y++) {
                        if (x < _levelData.Level.GetLength(0) && y < _levelData.Level.GetLength(1)) {
                            newArr[x, y] = _levelData.Level[x, y];
                        } else {
                            newArr[x, y] = "";
                        }
                    }
                }
                _levelData.Level = newArr;
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            GUILayout.Label("Objects");
            EditorGUILayout.Separator();

            GUILayout.Label("Temp Object");


            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            if (string.IsNullOrEmpty(_levelData.Name)) {
                EditorGUILayout.HelpBox("Please specify a [Name] for this object.", MessageType.Warning);
            } else {
                DrawCreateButton();
            }

            GUILayout.EndArea();
        }

        private void DrawCreateButton()
        {
            if (_overrideData != null) {
                if (GUILayout.Button("Save Asset", GUILayout.Height(40))) {
                    // TODO: Better way to copy information
                    _overrideData.Name = _levelData.Name;
                    _overrideData.Width = _levelData.Width;
                    _overrideData.Height = _levelData.Height;
                    _overrideData.Level = _levelData.Level;
                }
                EditorGUILayout.ObjectField(_overrideData, typeof(ObjectData), false);
            } else {
                if (GUILayout.Button("Create", GUILayout.Height(40))) {
                    string projectPath = string.IsNullOrEmpty(_lastPath) ? Application.dataPath : _lastPath;
                    string fullPath = EditorUtility.OpenFolderPanel("Select folder to save " + _levelData.Name + " to", projectPath, "");
                    if (fullPath.Length < projectPath.Length) {
                        return;
                    }
                    _lastPath = fullPath;
                    string path = "Assets" + fullPath.Remove(0, projectPath.Length);
                    Debug.Log(fullPath);
                    AssetDatabase.CreateAsset(_levelData, path + "/" + _levelData.Name + ".asset");
                }
            }
        }

        #endregion
    }
}

#endif