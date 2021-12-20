using UnityEngine;

public class EnemyKilledRoutine : MonoBehaviour
{
    [SerializeField] private GameObject BadSpider;
    public void DestroyEnemy()
    {
        Destroy(BadSpider);
    }
}
