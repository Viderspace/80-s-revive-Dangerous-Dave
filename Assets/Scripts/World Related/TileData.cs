using UnityEngine;
using UnityEngine.Tilemaps;

namespace World_Related
{
    [CreateAssetMenu]
    public class TileData : ScriptableObject
        /* Utility for Collectables-Manager script, holds info of each tile type in the game */
    {
        public TileBase[] relatedTiles;
        public bool isGun;
        public string tileName;
        public bool isDeadly;
        public bool isDoor;
        public bool isJetpack;
        public bool hasCoinValue;
        public int value;
        public bool unlocksDoor;


    }
}
