using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    #region Inspector
    
    [SerializeField] private Tilemap map;
    [SerializeField] private List<TileData> tileDatas;
    
    #endregion

    
    
    #region Fields
    private Dictionary<TileBase, TileData> dataFromTiles;
    
    private int PointsCollected { get; set; } = 0;
    
    #endregion





    #region MonoBehaviour

    


    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.relatedTiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = map.WorldToCell(mousePos);

            TileBase clickedTile = map.GetTile(gridPos);
            if(!dataFromTiles[clickedTile]) return;

            var tile = dataFromTiles[clickedTile];
            
            if (tile.hasCoinValue)
            {
                PointsCollected += tile.value;
                print("item collected, total points collected : "+ PointsCollected );
            }


            print("At position " + gridPos + " there is a " + clickedTile);
        }        
    }
    #endregion
}
