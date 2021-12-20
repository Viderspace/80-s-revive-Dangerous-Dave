using Dave_Related;
using UnityEngine;
namespace Enemies
{
    public class SpiderBehaviour : MonoBehaviour
        /* This script is responsible for the 'Bad spider' (game-object) behaviour and capabilities.
         * 
         * 'Bad-Spiders' are dynamic-enemy-objects. Every spider in the game constantly moves (non-stop) in circles
         *  on a pre-configured individual route.  Bad-Spiders can shoot bullets at dave,
         * one bullet for every 'recoilTime' amount of seconds. and they are deadly if dave collides with them.
         */
    {
        #region Inspector

        [SerializeField] private BezierFollow spiderRoute;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Animator animator;

        [SerializeField] private float spiderSpeed = 0.4f;

        [Header("Spider's Shooting Settings")] [SerializeField]
        private float bulletSpeed = 5f;

        [SerializeField] private float recoilTime = 5f;
        [SerializeField] private float semirRecoilTime = 2f;
        [SerializeField] private float howFarSpiderCanSee = 13f;

        #endregion

        #region Fields

        private GameObject _dave;
        private float _lastShotTime;
        private static readonly int SpiderKilled = Animator.StringToHash("SpiderKilled");

        /* these Const's below are preventing the spider's ability to shoot a bullet
         when they are located too low or too high */
        private const float MINBulletHeight = 0.0f;
        private const float MAXBulletHeight = 1.6f;

        #endregion

        #region Methods

        private void ShootBullet()
        {
            if (_lastShotTime > 0) return; // spiders can only shoot every 'recoilTime' amount of seconds

            // In the original game, the spider does not shoot when dave is too far from them
            if (Vector2.Distance(_dave.transform.position, transform.position) > howFarSpiderCanSee)
            {
                _lastShotTime = semirRecoilTime;
                return;
            }

            //This limitation below does not allow the spider to shoot when he is too low or too high (on the y axis)
            var spiderPos = gameObject.transform.position.y;
            if (spiderPos > MAXBulletHeight || spiderPos < MINBulletHeight) return;

            //When all the conditions above are met (timing, location etc..), the spider shoots a bullet
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed;
            _lastShotTime = recoilTime;
            Destroy(bullet, 3.5f);
        }

        #endregion

        #region MonoBehaviour

        private void Start()
        {
            _dave = FindObjectOfType<DaveController>().gameObject;
            spiderRoute.speedModifier = spiderSpeed;
        }


        private void Update()
            /* Time managing (for the spider's shooting pattern) */
        {
            if (_lastShotTime > 0)
            {
                _lastShotTime -= Time.deltaTime;
                return;
            }

            ShootBullet();
        }


        private void OnCollisionEnter2D(Collision2D other)
            /* When Dave collides with the spider, or when dave shoot at the spider,
             the spider stops moving, and his sprite is replaced with an 'explosion' animation,
             (the spider's collider is then disabled to avoid further unwanted collisions) */

        {
            if (!other.gameObject.CompareTag("Dave")) return;
            spiderRoute.speedModifier = 0;
            GetComponent<Collider2D>().enabled = false;
            _lastShotTime = 100f;
            animator.SetTrigger(SpiderKilled);
        }

        #endregion
    }
}