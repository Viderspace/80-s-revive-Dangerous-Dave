using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : MonoBehaviour
{
    [SerializeField]private Rigidbody2D _rigidbody;
    [SerializeField]private FeetOnGround _davesFeet;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _transform;
    private bool _isFacingRight;
    public float walkingSpeed =0.2f;
    private float _moveDirection;
    private bool _jump;

    public float jumpDuration = 0.5f;
    private float _jumpTime =  0.5f;

    private float _heightTarget;
    public float smoothTime = 1.3F;
    private Vector3 _velocity = Vector3.zero;


    public float jumpHeight = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _moveDirection = Input.GetAxisRaw("Horizontal") * walkingSpeed;
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && _davesFeet.OnGround)
        {
            _rigidbody.AddForce(transform.up * jumpHeight);
        }
    }

    private void SpriteDirection()
    //this function flips dave's character sprite, whenever he changes his walking direction;
    {
        if (_isFacingRight && _moveDirection < 0)
        {
            _spriteRenderer.flipX = true;
            _isFacingRight = false;
            return;
        }

        if (!_isFacingRight && _moveDirection > 0)
        {
            _spriteRenderer.flipX = false;
            _isFacingRight = true;
        }
    }

    private void FixedUpdate()
    {
        var localPosition = _transform.localPosition;
        localPosition = new Vector3( localPosition.x + _moveDirection , localPosition.y, 0);
        _transform.localPosition = localPosition;
        SpriteDirection();
    }

}
