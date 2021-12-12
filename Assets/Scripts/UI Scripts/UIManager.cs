using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _shared;
    
    [SerializeField] private GameManager gameManager;
    
    [Space][Header("UI Components")]
    [Header("---Upper Dashboard")]
    [SerializeField] private DigitBoard scoreBoard;
    [SerializeField] private DigitBoard levelBoard;
    [SerializeField] private LivesRemaining livesDisplay;
    
    
    [Space][Header("---Lower Dashboard")]
    [SerializeField] private GameObject goThruTheDoorImage;
    [SerializeField] private GameObject gunTextImage;
    [SerializeField] private GameObject jetpackTextImage;
    [SerializeField] private GameObject jetFuelIndicator;
    
    
    
    public void DisplayLives(int livesAmount)
    {
        livesDisplay.Lives = livesAmount;
    }

    public void Points(int num)
    {
        scoreBoard.UpdateBoard(num);
    }

    public void Level(int num)
    {
        levelBoard.UpdateBoard(num);
    }

    public void HasGun(bool value)
    {
        gunTextImage.SetActive(value);
    }

    public void NewJetpack()
    {
        jetpackTextImage.SetActive(true);
        jetFuelIndicator.SetActive(true);
        jetFuelIndicator.GetComponent<FuelStatus>().LoadFuel();
        
    }

    public void JetFuel(float jetFuelAmount)
    {
        if (jetFuelAmount <= 0)
            // Tank is empty, Turn off jetpack UI display
        {
            jetpackTextImage.SetActive(false);
            jetFuelIndicator.SetActive(false);
            return;
        }
        // Update Fuel indicator UI display (The red bars at the lower dashboard)
        jetFuelIndicator.GetComponent<FuelStatus>().DecreaseFuel(jetFuelAmount);
    }

    public void HasKey(bool value)
    {
        goThruTheDoorImage.SetActive(value);
        if (value) Debug.Log("GOT KEY! GO THRU THE DOOR!");
        
    }
    
    
    public void InitAll()
    {
        /* Init UI */
        scoreBoard.InitBoard();
        levelBoard.InitBoard();
        livesDisplay.InitLives();
    }

    private void Awake()
    {
        if (_shared == null)
        {
            _shared = this;
            // DontDestroyOnLoad(gameObject);
            // gameManager = FindObjectOfType<GameManager>();
        }
        else { Destroy(gameObject); }
        
    }
}
