using System.Collections.Generic;
using UnityEngine;


namespace Dave_Related
{
    public class DaveController : MonoBehaviour
    {
        #region Inspector

        [Header("Components")] 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private FeetOnGround davesFeet;
        [SerializeField] private Gun davesGun;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
    
        
        /* list below ("Right-Left Fire Points") contain 2 child-Objects, which keeps the relative start-position
         of bullet-instances (which dave shoots from, right or left).  */
        [SerializeField] private List<GameObject> bulletStartingPos = new List<GameObject>();
        
        
        [Space][Header("Movement Parameters")]
        
        [Range(0.01f, 0.2f)] [SerializeField] private float walkingSpeed;
        [Range(0.5f, 2.5f)] [SerializeField] private float jetSpeed;
        
        [SerializeField] private float jumpHeight;


        [Space][Header("Control Keys")]
        [SerializeField] public KeyCode jetKey = KeyCode.RightShift;
        [SerializeField] public KeyCode jumpKey = KeyCode.UpArrow;
        [SerializeField] public KeyCode shootKey = KeyCode.Space;
    
        #endregion

    
    
        #region Fields
    
        /* -- Animation variables --  */
        private static readonly int WalkingSpeed = Animator.StringToHash("Walking Speed");
        private static readonly int JetAnimation = Animator.StringToHash("JetOn");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int BlinkTrigger = Animator.StringToHash("BlinkTrigger");
        private static readonly int Explosion = Animator.StringToHash("Explosion");
        
        private const float SpawnFreezeTime = 2f;

        /* -- Physics variables --  */
        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private bool _isFacingRight;
        private bool _jump;
        private float _freezeMovement;


        #endregion

    
    
        #region Properties

        private float MoveDirection { get; set; }

        private bool JetMode { get; set; }
    
        
        #endregion

        

        #region Methods
    
        public void SpawnDave(Vector2 initPos)
        /* */
        {
            animator.SetTrigger(BlinkTrigger);
            IdleFreeze();
            transform.position = initPos;
        
        }

        private void Shoot()
        { 
            /* shooting a bullet from the correct location (see dave child objects) to the correct direction
         (derived from dave's sprite direction) */
            var gunLocation = (_isFacingRight) ? bulletStartingPos[0] : bulletStartingPos[1];
            davesGun.ShootBullet(_isFacingRight, gunLocation.transform.position);
        }
    

        private void MoveDave()
        {
            MoveDirection = Input.GetAxisRaw("Horizontal");


            if (Input.GetKey(shootKey) && gameManager.HasGun)
            {
                Shoot();
            }
        
            //enable Jet mode (& Apply Jetpack Physics)
            if (Input.GetKey(jetKey) && gameManager.JetFuelAmount > 0)
            {
                gameManager.JetFuelAmount -= Time.deltaTime;
                ActivateJet();
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
    
    
        private void SetSpriteDirection()
            // flips dave's Sprite whenever he changes his walking direction;
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
    
        private void IdleFreeze()
            /*Freeze's Dave Movement (for "SpawnDelay" seconds), and sets Animation clip to Idle */
        {
            _freezeMovement = SpawnFreezeTime;
            animator.SetFloat(WalkingSpeed, 0);
            animator.SetBool(IsJumping, false);
        }

        #endregion

        #region Jetpack Methods

        private void ActivateJet()
        {
            if (JetMode)
            {
                JetMovement();
                return;
            }
        
            _rigidbody.gravityScale = 0f;
            _rigidbody.velocity = Vector2.zero;
            animator.SetBool(JetAnimation, true);
            JetMode = true;
            JetMovement();
        }

        private void DeactivateJet()
        {
            JetMode = false;
            _rigidbody.gravityScale = 0.5f;
            animator.SetBool(JetAnimation, false);
        }

        private void JetMovement()
        {
            var verticalDirection = Input.GetAxisRaw("Vertical");
            _rigidbody.velocity = new Vector2(MoveDirection, verticalDirection) * jetSpeed;
        }

        #endregion
    

    
    
    
        #region MonoBehaviour

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    
        private void Update()
        {
            if (_freezeMovement > 0f)
            {
                _freezeMovement -= Time.deltaTime;
                return;
            }
            MoveDave();
        }

        private void FixedUpdate()
        {
            if (_freezeMovement > 0f) return;
        
            // Adjust Sprite direction
            SetSpriteDirection();
            if (JetMode) return;
        
            // Apply Walk/Jump animation
            animator.SetFloat(WalkingSpeed, Mathf.Abs(MoveDirection));
            animator.SetBool(IsJumping, !davesFeet.OnGround);
        
            // walk with normal Physics
            var pos = _transform.localPosition;
            _transform.localPosition = new Vector3(pos.x + (MoveDirection * walkingSpeed), pos.y, 0);
        }


        private void OnCollisionEnter2D(Collision2D other)
            /* Important:
         when Dave dies from any enemy, this triggers an animation clip (named "explosion").
          at the end of the animation clip there is a trigger (animation-event) which tells the game manager
           that dave just died. the game manager then spawns dave back to init position and takes care of the rest.
         amalek: 
         dave die -> explosion animation begins -> explosion ends -> animation event calling game manager's function "DaveDied()"   
         */
        {
            if (!other.gameObject.CompareTag("Enemy")) return;
            animator.SetTrigger(Explosion); 
            IdleFreeze();
            Debug.Log("explosion!");
        }

        #endregion
    }
}
