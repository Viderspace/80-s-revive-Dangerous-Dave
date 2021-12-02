using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetOnGround : MonoBehaviour
{
    private Collider2D _collider2D;

    public bool OnGround { get; set; }

    void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("World")) return;
        OnGround = true;
        Debug.Log("touching ground");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("World")) return;
        OnGround = false;
        Debug.Log("Left ground");
    }
    
}
