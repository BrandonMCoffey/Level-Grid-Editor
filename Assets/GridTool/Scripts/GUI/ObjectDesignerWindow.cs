using System.Linq;
using GridTool.Scripts;
using UnityEditor;
using UnityEngine;

public class ObjectDesignerWindow : EditorWindow
{
    private Rect _headerSection;
    private Texture2D _headerSectionTexture;
    private Color _headerSectionColor = new Color(0.5f, 0.5f, 0.5f);

    private Rect _loadSection;

    private Rect _mainSection;
    private Texture2D _mainSectionTexture;
    private Color _mainSectionColor = new Color(0.2f, 0.2f, 0.2f);

    private static ObjectData _objectData;
    private static ObjectData _loadObjectData;

    [MenuItem("Window/Object Designer")]
    private static void OpenWindow()
    {
        ObjectDesignerWindow window = (ObjectDesignerWindow)GetWindow(typeof(ObjectDesignerWindow));
        window.minSize = new Vector2(320, 320);
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
        _objectData = (ObjectData)ScriptableObject.CreateInstance(typeof(ObjectData));
    }

    // Called once or more during interactions with user
    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawLoad();
        DrawMain();
    }

    // Defines rect values and paints textures
    private void DrawLayouts()
    {
        int headerHeight = 50;
        int loadWidth = 100;
        int mainPadding = 10;

        _headerSection.x = 0;
        _headerSection.y = 0;
        _headerSection.width = Screen.width - loadWidth;
        _headerSection.height = headerHeight;

        _loadSection.x = Screen.width - loadWidth;
        _loadSection.y = 0;
        _loadSection.width = loadWidth;
        _loadSection.height = headerHeight;

        _mainSection.x = mainPadding;
        _mainSection.y = headerHeight + mainPadding;
        _mainSection.width = Screen.width - 2 * mainPadding;
        _mainSection.height = Screen.height - headerHeight - 2 * mainPadding;

        GUI.DrawTexture(_headerSection, _headerSectionTexture);
        GUI.DrawTexture(_loadSection, _headerSectionTexture);
        GUI.DrawTexture(_mainSection, _mainSectionTexture);
    }

    private void DrawHeader()
    {
        GUILayout.BeginArea(_headerSection);
        GUILayout.Label("Object Designer");
        GUILayout.EndArea();
    }

    private void DrawLoad()
    {
        GUILayout.BeginArea(_loadSection);
        EditorGUILayout.BeginVertical();
        _loadObjectData = (ObjectData)EditorGUILayout.ObjectField(_loadObjectData, typeof(ObjectData), false);
        if (GUILayout.Button("Load Existing")) {
            if (_loadObjectData != null) {
                // Confirmation Popup
                _objectData = ScriptableObject.Instantiate(_loadObjectData);
            } else {
                // Error Popup
            }
        }
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void DrawMain()
    {
        GUILayout.BeginArea(_mainSection);

        GUILayout.Label("Sprite Mode");
        _objectData.ObjectType = (ObjectSpriteType)EditorGUILayout.EnumPopup(_objectData.ObjectType);

        switch (_objectData.ObjectType) {
            case ObjectSpriteType.Static:
                DrawStaticSprite();
                break;
            case ObjectSpriteType.Directional:
                DrawDirectionalSprites();
                break;
            case ObjectSpriteType.Blendable:
                DrawBlendSprites();
                break;
        }

        GUILayout.Space(10);
        GUILayout.Label("Settings");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Sorting Priority");
        _objectData.SortingPriority = EditorGUILayout.IntField(_objectData.SortingPriority);
        EditorGUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    #region Sprites

    private static void DrawStaticSprite()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Sprite");
        _objectData.Static = (Sprite)EditorGUILayout.ObjectField(_objectData.Static, typeof(Sprite), false);
        EditorGUILayout.EndHorizontal();
    }

    private static void DrawDirectionalSprites()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("", GUILayout.MinWidth(0));
        _objectData.Up = (Sprite)EditorGUILayout.ObjectField(_objectData.Up, typeof(Sprite), false);
        EditorGUILayout.LabelField("", GUILayout.MinWidth(0));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        _objectData.Left = (Sprite)EditorGUILayout.ObjectField(_objectData.Left, typeof(Sprite), false);
        EditorGUILayout.LabelField("", GUILayout.MinWidth(0));
        _objectData.Right = (Sprite)EditorGUILayout.ObjectField(_objectData.Right, typeof(Sprite), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("", GUILayout.MinWidth(0));
        _objectData.Down = (Sprite)EditorGUILayout.ObjectField(_objectData.Down, typeof(Sprite), false);
        EditorGUILayout.LabelField("", GUILayout.MinWidth(0));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    private static void DrawBlendSprites()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        _objectData.TopLeft = (Sprite)EditorGUILayout.ObjectField(_objectData.TopLeft, typeof(Sprite), false);
        _objectData.Top = (Sprite)EditorGUILayout.ObjectField(_objectData.Top, typeof(Sprite), false);
        _objectData.TopRight = (Sprite)EditorGUILayout.ObjectField(_objectData.TopRight, typeof(Sprite), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        _objectData.MiddleLeft = (Sprite)EditorGUILayout.ObjectField(_objectData.MiddleLeft, typeof(Sprite), false);
        _objectData.Middle = (Sprite)EditorGUILayout.ObjectField(_objectData.Middle, typeof(Sprite), false);
        _objectData.MiddleRight = (Sprite)EditorGUILayout.ObjectField(_objectData.MiddleRight, typeof(Sprite), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        _objectData.BottomLeft = (Sprite)EditorGUILayout.ObjectField(_objectData.BottomLeft, typeof(Sprite), false);
        _objectData.Bottom = (Sprite)EditorGUILayout.ObjectField(_objectData.Bottom, typeof(Sprite), false);
        _objectData.BottomRight = (Sprite)EditorGUILayout.ObjectField(_objectData.BottomRight, typeof(Sprite), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    #endregion
}