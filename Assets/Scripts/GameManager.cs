using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _shared;

    #region Inspector

    [Space] [Header("Game Components")] [SerializeField]
    private GameObject uiPrefab;

    [SerializeField] private GameObject davePrefab;
    [SerializeField] private List<LevelData> levelsData = new List<LevelData>();
    private DoorTrigger _door;
    
    #endregion


    #region Fields

    private UIManager _uiManager;
    private GameObject _dave;

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
            _uiManager.Points(value);
        }
    }

    /* GAMEPLAY PROPERTIES */
    public bool HasGun
    {
        get => _hasGun;
        set
        {
            _hasGun = value;
            _uiManager.HasGun(value);
        }
    }


    public float JetFuelAmount
    {
        get => _jetFuelAmount;
        set
        {
            _jetFuelAmount = value;
            _uiManager.JetFuel(value);
        }
    }


    public bool HasKey
    {
        get => _hasKey;
        set
        {
            _hasKey = value;
            _uiManager.HasKey(value);
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
            _uiManager.Level(value);
        }
    }

    #endregion
    

    #region Methods

    public void CollectJetpack()
        // Jetpack Item has been collected, Turn on jetpack UI display
    {
        JetFuelAmount = JetFuelDuration;
        _uiManager.NewJetpack();
    }


    private void InitAllGameVariables()
    {
        /* Init Game Properties */
        CurrentLevel = 1;
        HasGun = false;
        HasKey = false;
        JetFuelAmount = 0f;
        TotalPointsCollected = 0;
        LivesRemaining = 3;
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
        _uiManager.DisplayLives(LivesRemaining);
        SpawnDaveToInitPos();
    }


    private void SpawnDaveToInitPos()
    {
        var targetPos = (levelsData[CurrentLevel-1]).daveInitPos;
        _dave.GetComponent<DaveController>().SpawnDave(targetPos);
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

    private void MakeUI()
    {
        var ui = Instantiate(uiPrefab, Vector3.zero, Quaternion.identity);
        _uiManager = ui.GetComponent<UIManager>();
        _uiManager.InitAll();
    }

    private void MakeDave()
    {
        _dave = Instantiate(davePrefab, Vector3.zero, Quaternion.identity);
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
        MakeUI();
        InitAllGameVariables();
        MakeDave();
        
        var firstLevel = SceneManager.GetSceneByBuildIndex(1);
        SceneManager.LoadSceneAsync("Level 1", LoadSceneMode.Additive);
        // SceneManager.SetActiveScene(firstLevel);
        
    }

    #endregion
}