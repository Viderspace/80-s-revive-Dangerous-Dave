
using UnityEngine;

public class DaveDiesRoutine : MonoBehaviour
/* This tiny script is an animation-Trigger (for dave's 'explosion' animation-clip)
 it calls Game manager function to start a "dave died" protocol */
{
    public GameManager gameManager;
    public void DaveExploded()
    {
        gameManager.DaveDied();
    }

    
}
