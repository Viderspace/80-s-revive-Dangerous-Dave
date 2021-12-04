
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollectablesManager : MonoBehaviour
{
    #region Inspector
    [SerializeField] private Tilemap map;
    [SerializeField] private List<TileData> tileTypes;
    
    [SerializeField] private float jetTime  = 10f;

    #endregion

    #region Fields
    
    private Dictionary<TileBase, TileData> dataFromTiles;
    
    
    // temp game vars  (will be moved to game manager)
    private int PointsCollected { get; set; } = 0;
    public bool hasKey;
    public bool hasJet;
    public float jetFuel = 0f;
    

    
    
    // ------private variables for finding a specific tile from tilemap------
    private Vector3Int _tileLocation;
    private readonly List<Vector3> _searchRadios = new List<Vector3>()
    {
        Vector3.right * 0.5f, Vector3.left * 0.5f,
        Vector3.up * 0.8f, Vector3.down * 0.8f,
        Vector3.right * 0.9f, Vector3.left * 0.9f,
        Vector3.up * 1.2f, Vector3.down * 1.2f,
        Vector3.zero,
    };

    #endregion

    #region Methods

    private Vector3Int GetTileLocation(GameObject dave)
        //this method receives dave's current location (on collision with a collectable tile),
        //searches the tilemap and returns the tile's grid location. 
    {
        var daveLocation = dave.transform.position;
        for (var i = 0; i < 9; i++)
        {
            _tileLocation = map.WorldToCell(daveLocation + _searchRadios[i]);
            if (map.GetTile(_tileLocation)) return _tileLocation;
        }

        Debug.Log("Collectable Tile not found");
        return _tileLocation = map.WorldToCell(daveLocation);
        
    }

    #endregion

    #region Monobehaviour
    
    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileTypes)
        {
            foreach (var tile in tileData.relatedTiles)  { dataFromTiles.Add(tile, tileData); }
        } 
    }
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Dave")) return;
        
        
        //finding the relevant tile location
        var targetLocation = GetTileLocation(other.gameObject);
        var tileObject = map.GetTile(targetLocation);
        //analyzing logic from the tile object
        if (tileObject != null) CollectTile(tileObject);
        // removing the tile from scene
        map.SetTile(targetLocation, null);
    }

    #endregion

    private void CollectTile(TileBase tileObject)
    {
        var tileInfo = dataFromTiles[tileObject];
        if (tileInfo.hasCoinValue)
        {
            PointsCollected += tileInfo.value;
            Debug.Log("collected " + tileInfo.value + "points, ("+PointsCollected + " total)");
        }

        if (tileInfo.unlocksDoor)
        {
            hasKey = true;
            Debug.Log("GOT KEY! GO TO THE DOOR!)");
        }

        if (tileInfo.isJetpack)
        {
            hasJet = true;
            jetFuel += jetTime;
        }
    }
    
    #region Dumpyard

    // void Start()
    // {
    //     BoundsInt bounds = map.cellBounds;
    //     TileBase[] allTiles = map.GetTilesBlock(bounds);
    //
    //     for (int x = 0; x < bounds.size.x; x++)
    //     {
    //         for (int y = 0; y < bounds.size.y; y++)
    //         {
    //             TileBase tile = allTiles[x + y * bounds.size.x];
    //             if (tile != null)
    //             {
    //                 _items.Add(new Vector3Int(x, y, 0), tile);
    //             }
    //         }
    //     }
    // }

    // Func<Vector2, Vector2, float> _isNear = (vec1, vec2) => (vec1- vec2).magnitude  ;
    //
    // private Vector3Int FindNearestTile(Vector2 daveLocation)
    // {
    //     var minDist = Mathf.Infinity;
    //     var nearestTile = Vector3Int.zero;
    //     foreach (var tile in _items.Keys)
    //     {
    //         var dist = Vector3.Distance(daveLocation, tile);
    //         if (dist < minDist)
    //         {
    //             nearestTile = tile;
    //             minDist = dist;
    //         }
    //     }
    //     return nearestTile;
    // }

    #endregion
}