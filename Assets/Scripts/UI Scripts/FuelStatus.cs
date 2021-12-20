using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class FuelStatus : MonoBehaviour
    {
        [SerializeField] private Image blackMask;


        private const float FullTank = 10f;
        private int _currentFuelDisplayed;


        public void LoadFuel()
        {
            blackMask.fillAmount = 0;
        }

        public void DecreaseFuel(float currentFuelInTank)
            /* Method Receives The Current Fuel Status From game manager,
         and updates the Jet Fuel display Indicator (at the Lower Dashboard) by decreasing the red Bars accordingly */
        {
            var incrementFactor = currentFuelInTank / FullTank;
            blackMask.fillAmount = 1 - incrementFactor;
        }





    }
}


