using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu]
public class TileData : ScriptableObject
{
    

    public TileBase[] relatedTiles;
    public string tileName;
    public bool isDeadly;
    public bool isDoor;
    public bool hasCoinValue;
    public int value;


}
