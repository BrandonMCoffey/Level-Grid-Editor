using GridTool.DataScripts;
using UnityEngine;

namespace GridTool.GameScripts
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private float _gridSize = 1f;
        [SerializeField] private bool _centered = true;
        [SerializeField] private ObjectController _baseObjectController = null;

        private int _width;
        private int _height;
        private Vector2 _bottomLeftCorner;
        private ObjectController[,] _objects;

        public void CreateGrid(int cellsWide, int cellsHigh)
        {
            // Clear old grid
            if (_objects != null && _objects.Length > 0) {
                foreach (var obj in _objects) {
                    if (obj != null) {
                        Destroy(obj.gameObject);
                    }
                }
            }
            _objects = new ObjectController[cellsWide, cellsHigh];
            _bottomLeftCorner = _centered ? -new Vector2(_gridSize * cellsWide / 2, _gridSize * cellsHigh / 2) : Vector2.zero;
            _width = cellsWide;
            _height = cellsHigh;
            DrawGrid();
        }

        public void AddObject(ObjectData obj, int x, int y)
        {
            if (x < 0 || x > _width || y < 0 || y > _height) {
                Debug.LogWarning("Attempted to Add object outside of grid bounds", gameObject);
                return;
            }
            if (_objects[x, y] != null) {
                Debug.LogWarning("Attempted to Add object on top of existing object", gameObject);
                return;
            }
            if (_baseObjectController != null) {
                _objects[x, y] = Instantiate(_baseObjectController, transform);
                _objects[x, y].gameObject.name = "Object_" + obj.Name;
            } else {
                Debug.LogWarning("No base object, creating objects is an expensive operation", gameObject);
                _objects[x, y] = new GameObject("Object_" + obj.Name, typeof(ObjectController)).GetComponent<ObjectController>();
                _objects[x, y].transform.SetParent(transform);
            }
            _objects[x, y].SetObject(obj);
            _objects[x, y].transform.localPosition = _bottomLeftCorner + new Vector2(_gridSize * (x + 0.5f), _gridSize * (y + 0.5f));
        }

        public void DrawGrid()
        {
            // Draw Rows
            Debug.DrawLine(_bottomLeftCorner, _bottomLeftCorner + new Vector2(_width, 0), Color.yellow, 100f);
            for (float y = _bottomLeftCorner.y + 1; y < _bottomLeftCorner.y + _height; y++) {
                Debug.DrawLine(new Vector2(_bottomLeftCorner.x, y), new Vector2(_bottomLeftCorner.x + _width, y), Color.white, 100f);
            }
            Debug.DrawLine(_bottomLeftCorner + new Vector2(0, _height), _bottomLeftCorner + new Vector2(_width, _height), Color.yellow, 100f);
            // Draw Columns
            Debug.DrawLine(_bottomLeftCorner, _bottomLeftCorner + new Vector2(0, _height), Color.yellow, 100f);
            for (float x = _bottomLeftCorner.x + 1; x < _bottomLeftCorner.x + _width; x++) {
                Debug.DrawLine(new Vector2(x, _bottomLeftCorner.y), new Vector2(x, _bottomLeftCorner.y + _height), Color.white, 100f);
            }
            Debug.DrawLine(_bottomLeftCorner + new Vector2(_width, 0), _bottomLeftCorner + new Vector2(_width, _height), Color.yellow, 100f);
        }
    }
}