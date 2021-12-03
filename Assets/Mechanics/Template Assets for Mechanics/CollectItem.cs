using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollectItem : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collected");
        if (collision.gameObject.CompareTag("Dave"))
        {
            Vector3 hitposition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitposition.x = hit.point.x - 0.01f * hit.normal.x;
                hitposition.y = hit.point.y - 0.01f * hit.normal.y;
                ;
                map.SetTile(map.WorldToCell(hitposition), null);
            }
        }
    }
}
        
