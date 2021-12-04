using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : MonoBehaviour
{
    #region Inspector

    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private FeetOnGround davesFeet;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    
    #endregion

    private bool _initIdle = true;
    private bool _isFacingRight;
    public float walkingSpeed =0.2f;
    private float _moveDirection;
    public float MoveDirection => _moveDirection;
    
    private bool _jump;

    public float jumpDuration = 0.5f;
    private float _jumpTime =  0.5f;

    private float _heightTarget;
    public float smoothTime = 1.3F;
    private Vector3 _velocity = Vector3.zero;


    public float jumpHeight = 3f;




    // Update is called once per frame
    void Update()
    {
        _moveDirection = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Walking Direction",_moveDirection);
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && davesFeet.OnGround)
        {
            rigidbody.AddForce(transform.up * jumpHeight);
        }
    }

    private void SpriteDirection()
    //this function flips dave's character sprite, whenever he changes his walking direction;
    {
        if (_initIdle && _moveDirection != 0f)
        {
            animator.SetTrigger("Exit Idle");
            _initIdle = false;
        }
        if (_isFacingRight && _moveDirection < 0)
        {
            spriteRenderer.flipX = true;
            _isFacingRight = false;
            return;
        }

        if (!_isFacingRight && _moveDirection > 0)
        {
            spriteRenderer.flipX = false;
            _isFacingRight = true;
        }
        
    }

    private void FixedUpdate()
    {
        var localPosition = _transform.localPosition;
        _transform.localPosition = new Vector3( localPosition.x + _moveDirection * walkingSpeed,
            localPosition.y, 0);
        SpriteDirection();
    }

}
