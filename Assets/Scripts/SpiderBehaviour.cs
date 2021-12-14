using System;
using UnityEngine;

public class SpiderBehaviour : MonoBehaviour
{

    #region Inspector

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float recoilTime = 5f;

    #endregion

    #region Fields

    private GameManager _gameManager;
    private float _lastShotTime;

    #endregion

    #region Methods

    private void ShootBullet()
    {
        if (_lastShotTime > 0) return;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed;
        _lastShotTime = recoilTime;
        Destroy(bullet, 5);
    }

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
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
        //TODO: Spider death  ANIMATION
        Destroy(gameObject);
    }

    #endregion


}
