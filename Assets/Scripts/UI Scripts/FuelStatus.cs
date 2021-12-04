using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FuelStatus : MonoBehaviour
{
    [SerializeField] private GameObject incrementPrefab;
    
    private readonly List<GameObject> _allIncrements = new List<GameObject>();
    private Vector3 _frameCenter;
    // private readonly Vector3 _originPos =  new Vector3(-3.75f, 0,-5) ;
    public int incrementsAmount;
    public float spacing = 0.127f;
    private int currentAmount;

    
    public void LoadFuel()
    {
        foreach (var bar in _allIncrements)
        {
            bar.SetActive(true);
        }
    }

    public void DecreaseFuel()
    {
        currentAmount -= 1;
        var increment = _allIncrements[currentAmount];
        increment.SetActive(false);
    }





    private void Awake()
    {
        _frameCenter = GetComponent<Transform>().position;
        currentAmount = incrementsAmount;
        for (var i = 0; i < incrementsAmount; i++)
        {
            var spawnPos = _frameCenter + new Vector3(
                -3.3f + i * (spacing), 0, -5);

            var bar = Instantiate(incrementPrefab, spawnPos, Quaternion.identity);
            bar.transform.parent = gameObject.transform;
            _allIncrements.Add(bar);
        }
    }
}
