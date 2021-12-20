using UnityEngine;
namespace Enemies
{
    public class Bullet : MonoBehaviour
    /* Script for Bullets Instances (from dave's Gun or monster's) to remove them on collision.
     (all other collision logic is run by The main scripts) */
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}
