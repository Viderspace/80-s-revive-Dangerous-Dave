using UnityEngine;
namespace Enemies
{
    public class DynamicEnemyRoute : MonoBehaviour
        /* A Short script i pulled from the web To make the monsters move in a specific Route/Pattern.
     This is how i was able to define in level 3 each 'bad-spider' specific route */
    {

        [SerializeField] private Transform[] controlPoints;
        private Vector2 _gizmosPosition;

        private void OnDrawGizmos()
        {
            for(float t = 0; t <= 1; t += 0.05f)
            {
                _gizmosPosition = 
                    Mathf.Pow(1 - t, 3) * controlPoints[0].position 
                    + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position 
                    + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position 
                    + Mathf.Pow(t, 3) * controlPoints[3].position;

                Gizmos.DrawSphere(_gizmosPosition, 0.1f);
            }
            Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y), new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
            Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y), new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));
        }
    }
}
