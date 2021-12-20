using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class FuelStatus : MonoBehaviour
        /* This script is responsible for adjusting the current-Fuel-In-tank Display  */
    {
        #region Inspector & Fields

        [SerializeField] private Image blackMask;
        private const float FullTank = 10f;
        private int _currentFuelDisplayed;

        #endregion

        #region Methods

        public void LoadFuel()
        {
            blackMask.fillAmount = 0;
        }

        public void DecreaseFuel(float currentFuelInTank)
            /* Method Receives The Current Fuel Status From game-manager,  and updates the Jet Fuel display Indicator
             (located at the Lower Dashboard only when a jetpack item is collected) 
             by decreasing the red Bars accordingly */
        {
            var incrementFactor = currentFuelInTank / FullTank;
            blackMask.fillAmount = 1 - incrementFactor;
        }

        #endregion
    }
}