using UnityEngine;

public class DeadlyTiles : MonoBehaviour
{
    // private GameObject _dave;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Dave")) return;
        Debug.Log("Deadly tiles are useless");
        
        // _gameManager.DaveDied();
        // TODO : Dave Dies animation

    }

    // private void Awake()
    // {
    //     _gameManager = FindObjectOfType<GameManager>();
    // }
}
