
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollectablesManager : MonoBehaviour
{
    #region Inspector
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Tilemap map;
    [SerializeField] private List<TileData> tileTypes;
    
    
    #endregion

    #region Fields
    
    private Dictionary<TileBase, TileData> _dataFromTiles;

    // ------private variables for finding a specific tile from tilemap-----
    //TODO : I Must come up with a more reliable way to track a specific tile location (on map) at collision time (AKA perform an item pickup)
    private Vector3Int _tileLocation;
    private readonly List<Vector3> _searchRadios = new List<Vector3>() {
        Vector3.right * 0.5f, Vector3.left * 0.5f,
        Vector3.up * 0.8f, Vector3.down * 0.8f,
        Vector3.right * 0.5f + Vector3.up * 0.5f,
        Vector3.right * 0.5f + Vector3.down * 0.5f,
        Vector3.left * 0.5f + Vector3.up * 0.8f,
        Vector3.left * 0.5f + Vector3.down * 0.8f,
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
        for (var i = 0; i < _searchRadios.Count; i++)
        {
            // Debug.Log("location: "+ map.WorldToCell(daveLocation + _searchRadios[i]));
            _tileLocation = map.WorldToCell(daveLocation + _searchRadios[i]);
            if (map.GetTile(_tileLocation)) return _tileLocation;
        }

        //TODO : I Must come up with a more reliable way to track a specific tile location (on map) at collision time (AKA perform an item pickup)
        Debug.Log("Collectable Tile not found");
        return _tileLocation = map.WorldToCell(daveLocation);
        
    }

    #endregion

    #region Monobehaviour
    
    private void Awake()
    {
        // Creating a dictionary With all Tile Objects paired with their Scriptable-Data Scripts
        _dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileTypes)
        {
            foreach (var tile in tileData.relatedTiles)  { _dataFromTiles.Add(tile, tileData); }
        } 
    }
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Dave")) return;
        
        //finding the relevant tile in Tilemap (via GetTileLocation() Method)
        var targetLocation = GetTileLocation(other.gameObject);
        var tileObject = map.GetTile(targetLocation);
        //Processing Tile info (via CollectTile() Method)
        if (tileObject != null) CollectTile(tileObject);
        // removing the tile from scene
        map.SetTile(targetLocation, null);
    }

    #endregion

    private void CollectTile(TileBase tileObject)
    {
        var tileInfo = _dataFromTiles[tileObject];
        
        if (tileInfo.hasCoinValue)
        {
            gameManager.TotalPointsCollected += tileInfo.value;
        }

        if (tileInfo.unlocksDoor)
        {
            gameManager.HasKey = true;
        }

        if (tileInfo.isJetpack)
        {
            gameManager.CollectJetpack();
        }

        if (tileInfo.isGun)
        {
            gameManager.HasGun = true;
        }
    }

}