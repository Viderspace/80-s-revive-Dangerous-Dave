using System.Collections.Generic;
using UnityEngine;
namespace UI_Scripts
{
    public class LivesRemaining : MonoBehaviour
    /* Simple Controller for Displaying the Current 'Lives' status in the Upper dashboard UI (aka 'DAVES: $ $ $')  */
    {
        #region Inspector & Fields

        [SerializeField] private List<GameObject> livesObjects = new List<GameObject>();

        private int _remainingLives;

        #endregion

        #region Properties

        public int Lives
        {
            get => _remainingLives;
            set
            {
                _remainingLives = value;
                if (Lives > 2 || Lives < 0) return;
                livesObjects[_remainingLives].SetActive(false);
            }
        }

        #endregion

        #region Methods

        public void InitLives()
        {
            foreach (var life in livesObjects)
            {
                life.SetActive(true);
            }

            Lives = 3;
        }

        #endregion

        #region MonoBehaviour

        private void Start()
        {
            InitLives();
        }

        #endregion
    }
}