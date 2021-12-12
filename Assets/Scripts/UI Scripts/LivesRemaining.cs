
using System.Collections.Generic;
using UnityEngine;

public class LivesRemaining : MonoBehaviour
{
    [SerializeField] private List<GameObject> livesObjects = new List<GameObject>();
    
    private int _remainingLives;
    public int Lives
    {
        get => _remainingLives;
        set
        {
            _remainingLives = value;
            if (Lives > 2 || Lives < 0) return;
            livesObjects[_remainingLives].SetActive(false);
        }
    }
    
    

    public void InitLives()
    {
        foreach (var life in livesObjects)
        {
            life.SetActive(true);
        }

        Lives = 3;
    }


    void Start()
    {
        InitLives();
    }
}