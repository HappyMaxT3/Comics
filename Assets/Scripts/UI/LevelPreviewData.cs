using UnityEngine;

namespace Main.UI
{
    [CreateAssetMenu(fileName = "LevelPreviewData", menuName = "Levels/Preview")]
    public class LevelPreviewData : ScriptableObject
    {
        public string Description;
        public string Name;
        public int SceneIndex;
        public bool IsAccessible;
    }
}
