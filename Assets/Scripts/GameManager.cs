using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _shared;

    #region Inspector
    
    [Space] [Header("Game Components")] [SerializeField]
    private UIManager uiManager;
    
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

    // private UIManager uiManager;
    
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

    public int CurrentLevel
    {
        get => _currentLevel;
        set
        {
            _currentLevel = value;
            uiManager.Level(value);
        }
    }

    public InitLevel StartFromLevel
    {
        get => startFromLevel;
        set
        {
            startFromLevel = value;
            Debug.Log("mamager:" + value);
            
        }
    }

    #endregion
    

    #region Methods

    public void CollectJetpack()
        // Jetpack Item has been collected, Turn on jetpack UI display
    {
        JetFuelAmount = JetFuelDuration;
        uiManager.NewJetpack();
    }


    private void InitAllGameVariables()
    {
        /* Init Game Properties */
        CurrentLevel = (int) StartFromLevel;
        HasGun = false;
        HasKey = false;
        JetFuelAmount = 0f;
        TotalPointsCollected = 0;
        LivesRemaining = 3;
        uiManager.gameObject.SetActive(true);
        uiManager.InitAll();
        
        dave.SetActive(true);
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
    {
        var targetPos = (levelsData[CurrentLevel-1]).daveInitPos;
        dave.GetComponent<DaveController>().SpawnDave(targetPos);
        mainCam.transform.position = levelsData[CurrentLevel - 1].camInitPos;
    }
    
    private void LoadFirstLevel()
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
        SpawnDaveToInitPos();

    }
    
    
    public void NextLevel()
    {
        CurrentLevel += 1;
        //TODO: FIND OUT IF THE ORIGINAL GAME ALLOWS TO KEEP GUN & JETPACK in the next level, IF SO, SET THEN TO 'false' HERE
        HasKey = false;
        HasGun = false;
        JetFuelAmount = 0f;
        SpawnDaveToInitPos();
    }






    private static void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    #endregion
    
    
    #region MonoBehaviour

    // This Game Manager class is a singleton;
    private void Awake()
    {
        if (_shared != null)
        {
            Destroy(gameObject);
            return;
        }

        _shared = this;
        // DontDestroyOnLoad(gameObject);
    }
    

    private void Start()
    {
        


    }

    public void PlayGame()
    {
        InitAllGameVariables();
        LoadFirstLevel();
        SpawnDaveToInitPos();
    }



    #endregion
}