using GridTool.DataScripts;
using UnityEngine;

namespace GridTool.GameScripts
{
    [RequireComponent(typeof(GridController))]
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelData _levelToLoad = null;

        private GridController _grid;

        private void Awake()
        {
            _grid = GetComponent<GridController>();
            if (_grid == null) {
                _grid = gameObject.AddComponent<GridController>();
            }
        }

        private void OnEnable()
        {
            Load();
        }

        public void Load()
        {
            Load(_levelToLoad);
        }

        public void Load(LevelData level)
        {
            level.CheckValid();
            _grid.CreateGrid(level.Width, level.Height);
            for (int x = 0; x < level.Width; x++) {
                for (int y = 0; y < level.Height; y++) {
                    var levelObject = level.Level[x, y].Name;
                    if (string.IsNullOrEmpty(levelObject)) continue;

                    var obj = level.Collection.GetObject(levelObject);
                    if (obj != null) {
                        _grid.AddObject(obj, x, y);
                    } else {
                        Debug.LogWarning("Could not find object '" + levelObject + "' in collection on level '" + level.Name + "'", gameObject);
                    }
                }
            }
        }
    }
}