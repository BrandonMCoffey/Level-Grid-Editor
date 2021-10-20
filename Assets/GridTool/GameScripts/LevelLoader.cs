using GridTool.DataScripts;
using UnityEngine;

namespace GridTool.GameScripts
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelData _levelToLoad = null;

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
            Debug.Log("Loading... " + level.Name);
        }
    }
}