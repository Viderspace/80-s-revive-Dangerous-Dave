using UnityEngine;

namespace Dave_Related
{
    public class FeetOnGround : MonoBehaviour
    /* Dave's Feet script: allows the main Controller script ('DaveController')
     to know whether the player touches  'ground' or not at any given moment in the game.
     this allows the player to perform a Jump only when dave is on the ground. */
    {
        #region Properties

        public bool OnGround { get; private set; }

        #endregion

        #region MonoBehaviour (Collisions)

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                OnGround = true;
                return;
            }

            if (!other.gameObject.CompareTag("World")) return;
            OnGround = true;
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("World")) return;
            OnGround = false;
        }

        #endregion
    }
}
