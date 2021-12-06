using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Transform))]
public class DaveController : MonoBehaviour
{
    #region Inspector

    [Header("Components")] 
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FeetOnGround davesFeet;
    [SerializeField] private Gun davesGun;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    /* list below contains 2 child game-Objects, which holds the origin location
     of which dave's gun shoot bullets from in both right and left direction.
     (hence the name: "Right-Left Fire Points") */
    [SerializeField] private List<GameObject> rlFirePoints = new List<GameObject>();
    
    
    [Space][Header("Move Parameters")]
    [Range(0.1f, 0.2f)] [SerializeField] private float walkingSpeed = 0.11f;
    [Range(0.01f, 0.2f)] [SerializeField] private float jetSpeed = 0.4f;
    [SerializeField] private float jumpHeight = 600;
    
    [Space][Header("Control Keys")]
    [SerializeField] private KeyCode jetKey = KeyCode.RightShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode shootKey = KeyCode.Space;
    
    #endregion

    
    
    #region Fields
    
    /* -- Animation variables --  */
    private static readonly int WalkingSpeed = Animator.StringToHash("Walking Speed");
    private static readonly int JetAnimation = Animator.StringToHash("JetOn");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    
    /* -- Physics variables --  */
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private bool _isFacingRight;
    private bool _jump;

    #endregion

    
    
    #region Properties

    private float MoveDirection { get; set; }

    private bool JetMode { get; set; }
    
    #endregion




    #region Methods

    private void SpriteDirection()
        // Method flips dave's sprite direction whenever he changes his walking direction;
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
        JetMode = true;
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = Vector2.zero;
        animator.SetBool(JetAnimation, true);
    }

    private void DeactivateJet()
    {
        JetMode = false;
        _rigidbody.gravityScale = 3;
        animator.SetBool(JetAnimation, false);
    }

    private void JetMovement()
    {
        var verticalDirection =  Input.GetAxisRaw("Vertical");
        var pos = _transform.position;
        _transform.position = new Vector3(pos.x + MoveDirection * jetSpeed,
            pos.y + verticalDirection * jetSpeed, 0);
    }

    #endregion
    
    
    
    
    #region MonoBehaviour

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        MoveDirection = Input.GetAxisRaw("Horizontal");
        
        //shoot a bullet (to the correct direction according to where dave is facing)
        if (gameManager.HasGun && Input.GetKey(shootKey))
        {
            var gunLocation = (_isFacingRight) ? rlFirePoints[0] : rlFirePoints[1];
            davesGun.ShootBullet(_isFacingRight, gunLocation.transform.position);
        }
        
        //enable Jet mode (& Apply Jetpack Physics)
        if (Input.GetKey(jetKey) && gameManager.JetFuelAmount > 0)
        {
            gameManager.JetFuelAmount -= Time.deltaTime;
            if (!JetMode) ActivateJet();
            JetMovement();
            return;
        }

        //disable Jet mode (& Continue with Normal physics)
        if (JetMode && (gameManager.JetFuelAmount <= 0 || !Input.GetKey(jetKey)))
        {
            DeactivateJet();
        }
        
        // Jump
        if (Input.GetKeyDown(jumpKey) && davesFeet.OnGround) 
        {
            _rigidbody.AddForce(Vector2.up * jumpHeight);
        }
    }
    


    private void FixedUpdate()
    {
        // Adjust Sprite direction
        SpriteDirection();
        if (JetMode) return;
        
        // Apply Walk/Jump animation
        animator.SetFloat(WalkingSpeed, Mathf.Abs(MoveDirection));
        animator.SetBool(IsJumping, !davesFeet.OnGround);
        
        // walk with normal Physics
        var pos = _transform.localPosition;
        _transform.localPosition = 
            new Vector3(pos.x + (MoveDirection * walkingSpeed), pos.y, 0);
        
    }

    #endregion
}
