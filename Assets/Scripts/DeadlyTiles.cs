using UnityEngine;

public class DeadlyTiles : MonoBehaviour
{
    private GameManager _gameManager;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Dave")) return;
        _gameManager.DaveDied();
        // TODO : Dave Dies animation

    }

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
}
