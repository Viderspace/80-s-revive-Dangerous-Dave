using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledRoutine : MonoBehaviour
{
    [SerializeField] private GameObject BadSpider;
    public void DestroyEnemy()
    {
        Destroy(BadSpider);
    }
}
