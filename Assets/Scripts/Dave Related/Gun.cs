using Unity.Mathematics;
using UnityEngine;
namespace Dave_Related
{
    public class Gun : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float recoilTime = 1.2f;

        #endregion

        #region Fields

        private float _lastShotTime;

        #endregion

        #region Methods

        public void ShootBullet(bool isFacingRight, Vector3 originPoint)
            /* method called by Dave's controller, spawns a bullet instance from dave's gun (if he has one). 
        the bullet direction is based on param "isFacingRight". 
        method respects a fixed recoil time (to avoid spraying bullets).  */
        {
            if (_lastShotTime > 0) return;
            var bullet = Instantiate(bulletPrefab, originPoint, quaternion.identity);
            switch (isFacingRight)
            {
                case false:
                    bullet.GetComponent<SpriteRenderer>().flipX = true;
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2((-1) * bulletSpeed, 0);
                    break;
                default:
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
                    break;
            }

            _lastShotTime = recoilTime;
            Destroy(bullet, 5);
        }

        #endregion

        #region MonoBehaviour

        private void Update()
        {
            if (_lastShotTime > 0)
            {
                _lastShotTime -= Time.deltaTime;
            }
        }

        #endregion
    }
}
