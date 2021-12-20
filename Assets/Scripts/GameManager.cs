using System.Collections.Generic;
using Dave_Related;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using World_Related;

public class GameManager : MonoBehaviour
{
    private static GameManager _shared;

    #region Inspector
    
    [Space] [Header("Game Components")] 
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject dave;
    [SerializeField] private List<LevelData> levelsData = new List<LevelData>();
    
    #endregion


    #region Fields
    
    [HideInInspector] public InitLevel startFromLevel = InitLevel.Level1;
    public enum InitLevel
    {
        Level1 = 1,
        Level2 = 2,
        Level3 = 3
    }

    private DoorTrigger _door;
    private const float JetFuelDuration = 10f;
    private int _totalPointsCollected;
    private float _jetFuelAmount;
    private bool _hasGun;
    private bool _hasKey;
    private bool _gameOver;
    private int _currentLevel;
    
    #endregion
    
    
    #region Properties

    /* UI PROPERTIES */
    public int TotalPointsCollected
    {
        get => _totalPointsCollected;
        set
        {
            _totalPointsCollected = value;
            uiManager.Points(value);
        }
    }
    
    
    public InitLevel StartFromLevel
    {
        get => startFromLevel;
        set => startFromLevel = value;
    }

    /* GAMEPLAY PROPERTIES */
    public bool HasGun
    {
        get => _hasGun;
        set
        {
            _hasGun = value;
            uiManager.HasGun(value);
        }
    }


    public float JetFuelAmount
    {
        get => _jetFuelAmount;
        set
        {
            _jetFuelAmount = value;
            uiManager.JetFuel(value);
        }
    }


    public bool HasKey
    {
        get => _hasKey;
        set
        {
            _hasKey = value;
            uiManager.HasKey(value);
            _door = FindObjectOfType<DoorTrigger>();
            if (_door) _door.Locked(value);
        }
    }
    
    private int LivesRemaining { get; set; } = 3;

    private int CurrentLevel
    {
        get => _currentLevel;
        set
        {
            _currentLevel = value;
            uiManager.Level(value);
        }
    }


    #endregion
    

    #region Methods

    public void CollectJetpack()
        /* Setting Fuel Amount to Full-Tank, and displaying 'jetpack' UI in lower dashboard.  */
    {
        JetFuelAmount = JetFuelDuration;
        uiManager.NewJetpack();
    }


    private void InitAllGameVariables()
        /* Initialise all Game Properties at the beginning of the game */
    {
        CurrentLevel = (int) StartFromLevel;
        HasGun = false;
        HasKey = false;
        JetFuelAmount = 0f;
        TotalPointsCollected = 0;
        LivesRemaining = 3;
        uiManager.gameObject.SetActive(true);
        uiManager.InitAll();
        // dave.SetActive(true);
    }


    public void DaveDied()
    {
        if (LivesRemaining <= 0)
        {
            Debug.Log("Game Over");
            QuitGame();
            return;
        }
        
        LivesRemaining -= 1;
        uiManager.DisplayLives(LivesRemaining);
        SpawnDaveToInitPos();
    }


    private void SpawnDaveToInitPos()
    /* Spawning Dave to the current level Init position,
     For every level, The Init Position is stored inside a 'Level Data' (Scriptable object) */
    {
        if (dave.gameObject.activeSelf == false)  dave.SetActive(true);
        var targetPos = (levelsData[CurrentLevel-1]).daveInitPos;
        dave.GetComponent<DaveController>().SpawnDave(targetPos);
        mainCam.transform.position = levelsData[CurrentLevel - 1].camInitPos;


        
    }
    
    private void LoadInitLevel()
        /* Loading (Additively) the Scene of the selected Starting level.
         (In the start-up UI menu, the player can choose from what level to start the game). */
    {
        switch (StartFromLevel)
        {
            case InitLevel.Level1:
                SceneManager.LoadSceneAsync("Level 1", LoadSceneMode.Additive);
                break;
            case InitLevel.Level2:
                SceneManager.LoadSceneAsync("Level 2", LoadSceneMode.Additive);
                break;
            case InitLevel.Level3:
                SceneManager.LoadSceneAsync("Level 3", LoadSceneMode.Additive);
                break;
            default:
                SceneManager.LoadSceneAsync("Level 1", LoadSceneMode.Additive);
                break;
        }
        CurrentLevel = (int)StartFromLevel;
    }
    
    
    public void NextLevel()
    {
        CurrentLevel += 1;
        HasKey = false;
        HasGun = false;
        JetFuelAmount = 0f;
        SpawnDaveToInitPos();
    }

    
    public static void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    
    public void PlayGame()
    {
        InitAllGameVariables();
        LoadInitLevel();
        SpawnDaveToInitPos();
    }

    #endregion
    
    
    #region MonoBehaviour

    
    private void Awake()
    {
        
        if (_shared != null) // This Game Manager is a singleton;
        {
            Destroy(gameObject);
            return;
        }
        _shared = this;
    }

    #endregion
}