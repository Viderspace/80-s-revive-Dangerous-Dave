using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField]private FeetOnGround _davesFeet;

    private float movespeed =0.2f;
    private float moveDirection;
    private bool jump;

    public float jumpduration = 0.5f;
    private float jumptime =  0.5f;

    private float heightTarget;
    public float smoothTime = 1.3F;
    private Vector3 velocity = Vector3.zero;


    public float jumpHeight = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = transform.localPosition.x + Input.GetAxis("Horizontal") * movespeed;
        if (Input.GetKeyDown(KeyCode.UpArrow) && _davesFeet.OnGround)
        {
            Debug.Log("jump");
            heightTarget = transform.localPosition.y + jumpHeight;
            jumptime = jumpduration;
        }
    }

    private void FixedUpdate()
    {
        if (jumptime > 0) 
        {
            jumptime -= Time.deltaTime;
            var y = Vector3.SmoothDamp(transform.localPosition, new Vector3(
                moveDirection,
                heightTarget, 0), ref velocity, smoothTime).y;

            transform.localPosition = new Vector3(moveDirection, y, 0);
            return;
        }
        
        transform.localPosition = new Vector3( moveDirection, 
            transform.localPosition.y, 0);
    }

}
