using System;
using UnityEngine;


public class DaveController : MonoBehaviour
{
    #region Inspector
    
    [Header("Components")]
    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private FeetOnGround davesFeet;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    
    
    [Space][Header("Move Parameters")]
    
    [Range(0.1f, 0.2f)] [SerializeField] private float walkingSpeed = 0.11f;
    [Range(0.01f, 0.2f)] [SerializeField] private float jetSpeed = 0.8f;
    [SerializeField] private float jumpHeight = 600;
    
    [Space][Header("Control Keys")]
    
    [SerializeField] private KeyCode jetKey = KeyCode.RightShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.UpArrow;
    
    
    
    #endregion

    
    #region Fields
    
    
    
    private static readonly int WalkingSpeed = Animator.StringToHash("Walking Speed");
    private static readonly int JetAnimation = Animator.StringToHash("JetOn");
    // -- Animation variables --
    private bool _isFacingRight;
    private bool _jump;
    private float _heightTarget;
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");

    #endregion

    
    
    
    #region Properties

    private float MoveDirection { get; set; }

    public bool JetOn { get; set; }

    public float JetFuel { get; set; } = 0;

    #endregion




    #region Methods

    private void SpriteDirection()
        // flips dave's sprite whenever he changes his walking direction;
    {
        switch (_isFacingRight)
        {
            case true when MoveDirection < 0:
                spriteRenderer.flipX = true;
                _isFacingRight = false;
                return;
            
            case false when MoveDirection > 0:
                spriteRenderer.flipX = false;
                _isFacingRight = true;
                break;
        }
    }


    private void ActivateJet()
    {
        JetOn = true;
        rigidbody.gravityScale = 0;
        rigidbody.velocity = Vector2.zero;
        animator.SetBool(JetAnimation, true);
            

    }

    private void DeactivateJet()
    {
        JetOn = false;
        rigidbody.gravityScale = 3;
        animator.SetBool(JetAnimation, false);
        

    }

    private void JetMovement()
    {
        var vecticalDirection =  Input.GetAxisRaw("Vertical");
        
        var pos = _transform.position;
        _transform.position = new Vector3(
            pos.x + MoveDirection * jetSpeed,
            pos.y + vecticalDirection * jetSpeed, 0);
    }

    #endregion
    
    
    
    
    #region MonoBehaviour

    private void Start()
    {
        JetFuel = 100f;
    }

    private void Update()
    {
        MoveDirection = Input.GetAxisRaw("Horizontal");
        
        //enable Jet mode
        if (Input.GetKey(jetKey) && JetFuel > 0)
        {
            JetFuel -= Time.deltaTime;
            
            if (!JetOn) ActivateJet();
            JetMovement();
            return;
        }

        //disable Jet mode
        if (JetOn && (JetFuel <= 0 || !Input.GetKey(jetKey)))
        {
            DeactivateJet();
        }
        
        // Jump
        if (Input.GetKeyDown(jumpKey) && davesFeet.OnGround) 
        {
            rigidbody.AddForce(Vector2.up * jumpHeight);
        }
    }
    


    private void FixedUpdate()
    {
        SpriteDirection();
        if (JetOn) return;
        
        animator.SetFloat(WalkingSpeed, Mathf.Abs(MoveDirection));
        animator.SetBool(IsJumping, !davesFeet.OnGround);
        
        var pos = _transform.localPosition;
        _transform.localPosition = 
            new Vector3(pos.x + (MoveDirection * walkingSpeed), pos.y, 0);
        
    }

    #endregion
}
