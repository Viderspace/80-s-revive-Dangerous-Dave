using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Inspector

    [Header("UI Components")]
    [Header("---Upper Dashboard")]
    [SerializeField] private DigitBoard scoreBoard;
    [SerializeField] private DigitBoard levelBoard;
    [SerializeField] private LivesRemaining livesDisplay;
    
    
    [Space][Header("---Lower Dashboard")]
    [SerializeField] private GameObject goThruTheDoorImage;
    [SerializeField] private GameObject gunTextImage;
    [SerializeField] private GameObject jetpackTextImage;
    [SerializeField] private GameObject jetFuelIndicator;
    
    [Space][Header("Game Components")]
    // [SerializeField] private DaveController dave;
    [SerializeField] private CollectablesManager collectablesManager;
    // [SerializeField private TriggersManager;

    [Space] [Header("Game Parameters")] 
    [Tooltip("Jet Fuel Duration : The amount of seconds dave can use Jet, from a single Jetpack Item ")]
    
    
    #endregion

    #region Fields
    private const float JetFuelDuration = 10f;
    private int _totalPointsCollected;
    private int _currentLevel;
    private float _jetFuelAmount;
    private bool _hasGun;
    private bool _hasKey;
    


    #endregion

    #region Properties
    
    /* UI PROPERTIES */
    public int TotalPointsCollected
    {
        get => _totalPointsCollected;
        set
        {
            _totalPointsCollected = value; 
            scoreBoard.UpdateBoard(value);
            
        }

    }
    /* GAMEPLAY PROPERTIES */
    public bool HasGun
    {
        get => _hasGun;
        set
        {
            _hasGun = value;
            gunTextImage.SetActive(value);
        }
    }
    

    public float JetFuelAmount
    {
        get => _jetFuelAmount;
        set
        {
            _jetFuelAmount = value;
            if (_jetFuelAmount <= 0)
                // Tank is empty, Turn off jetpack UI display
            {
                jetpackTextImage.SetActive(false);
                jetFuelIndicator.SetActive(false);
                return;
            }
            // Update Fuel indicator UI display (The red bars at the lower dashboard)
            jetFuelIndicator.GetComponent<FuelStatus>().DecreaseFuel(_jetFuelAmount);
            
        }
    }

    public bool HasKey
    {
        get => _hasKey;
        set
        {
            _hasKey = value;
            goThruTheDoorImage.SetActive(value);
            if(value) Debug.Log("GOT KEY! GO THRU THE DOOR!");
        }
    }

    #endregion

    #region Methods

    public void CollectJetpack()
        // Jetpack Item has been collected, Turn on jetpack UI display
    {
        JetFuelAmount = JetFuelDuration;
        jetpackTextImage.SetActive(true);
        jetFuelIndicator.SetActive(true);
        jetFuelIndicator.GetComponent<FuelStatus>().LoadFuel();
    }

    private void InitGame()
    {
        /* Init UI */
        scoreBoard.InitBoard();
        levelBoard.InitBoard();
        livesDisplay.InitLives();
        
        /* Init Game Properties */
        HasGun = false;
        HasKey = false;
        JetFuelAmount = 0f;
        TotalPointsCollected = 0;
        
        //TODO : a "SpawnDave()" Method, Holds the starting position of dave on map for every level
        
    }



    #endregion


    #region MonoBehaviour

    private void Start()
    {
        InitGame();
    }

    #endregion


}
