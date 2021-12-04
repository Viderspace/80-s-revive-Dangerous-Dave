
using System.Collections.Generic;
using UnityEngine;

public class LivesRemaining : MonoBehaviour
{
    [SerializeField] private List<GameObject> livesObjects = new List<GameObject>();
    
    private int _remainingLives = 3;
    public int Lives
    {
        get => _remainingLives;
        set
        {
            _remainingLives = value;
            livesObjects[_remainingLives].SetActive(false);
        }
    }

    public void InitLives()
    {
        foreach (var life in livesObjects)
        {
            life.SetActive(true);
        }

        _remainingLives = 3;
    }


    void Start()
    {
        InitLives();
    }
}