using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI_Scripts
{
    public class DigitBoard : MonoBehaviour
        /* Logic script for Displaying Numbers. (the Sum for Points collected / The Current level played).
         Instead of finding a 'good enough' similar font for the UI Display, i made this logic and applied the 
         Original Numeral-graphics of the game here. */
    {
        #region Inspector

        /* list contains the board image-objects (arranged from right to left) */
        [SerializeField] private List<Image> digitsObjects = new List<Image>();

        /*  list contains all (0-9) numeral digits sprites in order */
        [SerializeField] private List<Sprite> digitsSprites = new List<Sprite>();

        /*  The original game has limit for the digit displays.
         i changed each bord max val here (onr for the score and one for the level-count) here */
        [SerializeField] private int maxValue = 99;

        #endregion
        
        #region Methods

        public void InitBoard()
        {
            foreach (var digit in digitsObjects)
            {
                digit.sprite = digitsSprites[0];
            }
        }

        public void UpdateBoard(int num)
        {
            /* --- input tests --- */
            if (num < 0)
            {
                Debug.Log("Faulty input at digit-board");
                return;
            }

            if (num > maxValue)
            {
                num = maxValue;
            }


            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < digitsObjects.Count; i++)
            {
                var currentDig = num % 10;
                digitsObjects[i].sprite = digitsSprites[currentDig];
                if (num > 0) num /= 10;
            }
        }

        #endregion
    }
}