using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FuelStatus : MonoBehaviour
{
    #region Inspector
    [SerializeField] private GameObject incrementPrefab;
    [SerializeField] private Transform firstBarLocation;
    [SerializeField] private Transform lastBarLocation;
    [Space][Header("design parameters")]
    public float spacing = 0.127f;
 
    
    #endregion
    
    
    #region Fields
    private  int _incrementsAmount;
    private const float FullTank = 10f;
    private readonly List<GameObject> _allIncrements = new List<GameObject>();
    
    private Vector3 _frameCenter;
    private int _currentFuelDisplayed;
    
    #endregion

    
    public void LoadFuel()
    {
        foreach (var increment in _allIncrements)
        {
            increment.SetActive(true);
        }
    }

    public void DecreaseFuel(float currentFuelInTank)
    /* Method Receives The Current Fuel Status From game manager,
     and updates the Jet Fuel display Indicator (at the Lower Dashboard) by decreasing the red Bars accordingly */
    {
        var incrementFactor = FullTank / _allIncrements.Count();
        if (!(_currentFuelDisplayed * incrementFactor > currentFuelInTank)) return;
        _currentFuelDisplayed -= 1;
        var highestIncrement = _allIncrements[_currentFuelDisplayed+1];
        highestIncrement.SetActive(false);



    }





    private void Awake()
    {
        _frameCenter = GetComponent<Transform>().position;
        _incrementsAmount = 0;
        while (_allIncrements.Count * spacing < lastBarLocation.position.x - firstBarLocation.position.x)
        {
            var spawnPos = firstBarLocation.position + new Vector3(
                _allIncrements.Count * (spacing), 0, -5);
            // _incrementsAmount++;

            var bar = Instantiate(incrementPrefab, spawnPos, Quaternion.identity);
            bar.transform.parent = gameObject.transform;
            _allIncrements.Add(bar);
        }
        _incrementsAmount = _allIncrements.Count;
        _currentFuelDisplayed = _allIncrements.Count;


        // for (var i = 0; i < _incrementsAmount; i++)
        // {
        //     var spawnPos = originLocation.position + new Vector3(
        //           i * (spacing), 0, -5);
        //
        //     var bar = Instantiate(incrementPrefab, spawnPos, Quaternion.identity);
        //     bar.transform.parent = gameObject.transform;
        //     _allIncrements.Add(bar);
        // }
    }
}
