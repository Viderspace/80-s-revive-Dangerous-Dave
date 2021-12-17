using System;
using UnityEngine;

public class SpiderBehaviour : MonoBehaviour
{

    #region Inspector

    [SerializeField] private float spiderSpeed = 0.4f;
    [SerializeField] private BezierFollow spiderRoute;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;
    [Header("Spider Shooting Settings")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float recoilTime = 5f;
    [SerializeField] private float semirRecoilTime = 2f;
    [SerializeField] private float minBulletHeight = 0.0f;
    [SerializeField] private float maxBulletHeight = 1.6f;
    [SerializeField] private float howFarSpiderCanSee = 13f;

    #endregion

    #region Fields

    private GameObject _dave;
    private float _lastShotTime;
    private static readonly int SpiderKilled = Animator.StringToHash("SpiderKilled");

    #endregion

    #region Methods

    private void ShootBullet()
    {
        
        if (_lastShotTime > 0) return;
        if (Vector2.Distance(_dave.transform.position, transform.position) > howFarSpiderCanSee)
        { // In the original game, the spider does not shoot when dave is too far.
            _lastShotTime = semirRecoilTime;
            return;
        }
        
        //This limitation below does not allow the spider to shoot when he is too low or too high (on the y axis)
        var spiderPos = gameObject.transform.position.y; 
        if(spiderPos > maxBulletHeight || spiderPos < minBulletHeight ) return;

        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed;
        _lastShotTime = recoilTime;
        Destroy(bullet, 5);
    }

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        _dave = FindObjectOfType<DaveController>().gameObject;
        spiderRoute.speedModifier = spiderSpeed;
    }

    private void Update()
    {
        if (_lastShotTime > 0)
        {
            _lastShotTime -= Time.deltaTime;
            return;
        }
        ShootBullet();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Dave")) return;
        spiderRoute.speedModifier = 0;
        GetComponent<Collider2D>().enabled = false;
        _lastShotTime = 100f;
        animator.SetTrigger(SpiderKilled);
        //TODO: Spider death  ANIMATION
        
    }

    #endregion


}
