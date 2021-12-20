using UI_Scripts;
using UnityEngine;

namespace Mechanics.Lives
{
    public class DummyManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;

        private int _lives = 3;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _lives -= 1;
                uiManager.DisplayLives(_lives);
            }
        }
    }
}
