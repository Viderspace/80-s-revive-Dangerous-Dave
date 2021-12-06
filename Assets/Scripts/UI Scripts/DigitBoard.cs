using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitBoard : MonoBehaviour
{
    // list contains the board digit-objects, ** arranged from right to left **
    [SerializeField] private List<DynamicDigit> digitsObjects = new List<DynamicDigit>();
    
    // list contains all (0-9) numeral digits sprites in order
    [SerializeField] private List<Sprite> digitsSprites = new List<Sprite>();

    [SerializeField] private int maxValue = 99;


    public void InitBoard()
    {
        foreach (var digit in digitsObjects)
        {
            digit.ReplaceDigit(digitsSprites[0]);
        }
    }
    
    public void UpdateBoard(int num)
    {
        /* --- input tests --- */
        if (num < 0)
        {
            Debug.Log("Faulty input at digitboard");
            return;
        }
        if (num > maxValue) { num = maxValue; }
        

        for (var i = 0; i < digitsObjects.Count; i++)
        {
            var currentDig = num % 10;
            digitsObjects[i].ReplaceDigit(digitsSprites[currentDig]);
            if (num>0) num /= 10;
        }
    }
}
