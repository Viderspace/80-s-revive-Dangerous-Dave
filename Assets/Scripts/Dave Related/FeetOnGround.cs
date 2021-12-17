using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetOnGround : MonoBehaviour
{
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private Animator animator;
    private bool _onGround;
    public bool OnGround
    {
        get => _onGround;
        set
        {
            _onGround = value;
            


        } 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnGround = true;
            return;
        }

        if (!other.gameObject.CompareTag("World")) return;
        OnGround = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("World")) return;
        OnGround = false;
    }
    
}
