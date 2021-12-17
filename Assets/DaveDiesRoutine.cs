
using UnityEngine;

public class DaveDiesRoutine : MonoBehaviour
{
    public GameManager gameManager;
    public void DaveExploded()
    {
        gameManager.DaveDied();
    }

    
}
