using System.Collections;
using UnityEngine;
namespace Enemies
{
    public class BezierFollow : MonoBehaviour
        /* A Short script i pulled from the web To make the monsters move in a specific Route/Pattern.
         This is how i was able to define in level 3 each 'bad-spider' specific route */
    {
        #region Inspector & Fields

        [SerializeField] private Transform[] routes;
        private int _routeToGo;
        private float _tParam;
        private Vector2 _objectPosition;
        [HideInInspector] public float speedModifier = 0.4f;
        private bool _coroutineAllowed;

        #endregion

        #region Methods

        private IEnumerator GoByTheRoute(int routeNum)
        {
            _coroutineAllowed = false;

            Vector2 p0 = routes[routeNum].GetChild(0).position;
            Vector2 p1 = routes[routeNum].GetChild(1).position;
            Vector2 p2 = routes[routeNum].GetChild(2).position;
            Vector2 p3 = routes[routeNum].GetChild(3).position;

            while (_tParam < 1)
            {
                _tParam += Time.deltaTime * speedModifier;

                _objectPosition = Mathf.Pow(1 - _tParam, 3) * p0 + 3 * Mathf.Pow(1 - _tParam, 2) * _tParam * p1 +
                                  3 * (1 - _tParam) * Mathf.Pow(_tParam, 2) * p2 + Mathf.Pow(_tParam, 3) * p3;

                transform.position = _objectPosition;
                yield return new WaitForEndOfFrame();
            }

            _tParam = 0f;

            _routeToGo += 1;

            if (_routeToGo > routes.Length - 1)
            {
                _routeToGo = 0;
            }

            _coroutineAllowed = true;
        }

        #endregion

        # region MonoBehavior

        private void Start()
        {
            _routeToGo = 0;
            _tParam = 0f;
            speedModifier = 0.4f;
            _coroutineAllowed = true;
        }

        private void Update()
        {
            if (_coroutineAllowed)
            {
                StartCoroutine(GoByTheRoute(_routeToGo));
            }
        }

        #endregion
    }
}