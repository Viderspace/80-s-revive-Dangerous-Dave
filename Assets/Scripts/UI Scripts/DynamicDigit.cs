using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDigit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;


    public void ReplaceDigit(Sprite newDigit)
    {
        spriteRenderer.sprite = newDigit;
    }



}
