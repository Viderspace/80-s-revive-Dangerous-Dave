using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu]
public class TileData : ScriptableObject
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
