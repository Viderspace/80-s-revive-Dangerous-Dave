using UnityEngine;

namespace World_Related
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    /* Utility for holding key-info's , of each level in the game */
    {
        public Vector3 camInitPos;
        public bool endGame;
        public Vector2 daveInitPos;
        public string prevLevelName;
    }
}
