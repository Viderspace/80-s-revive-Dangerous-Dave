
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public GameManager gameManager;
    public DaveController daveController;

    private Dictionary<int, KeyCode> _jumpKeys = new Dictionary<int, KeyCode>()
    {
        {0, KeyCode.UpArrow}, {1, KeyCode.Backspace}, {2, KeyCode.LeftControl}
    };
    
    private Dictionary<int, KeyCode> _jetpackKeys = new Dictionary<int, KeyCode>()
    {
        {0, KeyCode.LeftShift}, {1, KeyCode.X}, {2, KeyCode.LeftAlt}
    };
    
    private Dictionary<int, KeyCode> _fireKeys = new Dictionary<int, KeyCode>()
    {
        {0, KeyCode.Backspace}, {1, KeyCode.Z}
    };
    

    public void SetJumpKey(int key)
    {
        Debug.Log(_jumpKeys[key]);
        daveController.jumpKey = _jumpKeys[key];
    }
    public void SetJetpackKey(int key)
    {
        Debug.Log(_jetpackKeys[key]);
        daveController.jetKey = _jetpackKeys[key];
    }
    public void SetFireKey(int key)
    {
        Debug.Log(_fireKeys[key]);
        daveController.shootKey = _fireKeys[key];
    }
    
    public void EnableSound(bool value)
    {
        AudioListener.volume = value ? 1f : 0f;
    }

    public void SelectLevel(int levelIndex)
    {
        Debug.Log("Level selected: "+ levelIndex);
        gameManager.StartFromLevel = (GameManager.InitLevel) levelIndex + 1;
    }

    private void Start()
    {
        gameManager.StartFromLevel = (GameManager.InitLevel) 1;
    }
}
