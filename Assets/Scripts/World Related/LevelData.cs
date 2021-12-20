using UnityEngine;

namespace World_Related
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        public Vector3 camInitPos;
        public bool endGame;
        public Vector2 daveInitPos;
        public string prevLevelName;
    }
}
