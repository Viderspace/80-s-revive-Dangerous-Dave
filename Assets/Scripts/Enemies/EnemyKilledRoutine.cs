using UnityEngine;
namespace Enemies
{
    public class EnemyKilledRoutine : MonoBehaviour
    {
        [SerializeField] private GameObject badSpider;
        public void DestroyEnemy()
        {
            Destroy(badSpider);
        }
    }
}
