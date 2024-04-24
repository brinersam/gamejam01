using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour
{
    [SerializeField] private Light2D _lightObj;
    [SerializeField] private int _fuelMax = 120;
    private int __fuelCurrent;

    private float _maxRadius;
    private int CurFuel {get => __fuelCurrent; set {__fuelCurrent = FuelWatcher(value);}}
    
    public event Action<float> OnFuelChange;

    private void Awake()
    {
        _maxRadius = _lightObj.pointLightOuterRadius;
        __fuelCurrent = _fuelMax;  
    }

    private void Start()
    {
        System_Ticker.Instance.OnSecond += Tick;
    }

    public void StopTorchFromRunningOut()
    {
        System_Ticker.Instance.OnSecond -= Tick;
    }

    public void Restore()
    {
        CurFuel = _fuelMax;
    }

    public void RefillPct(float pct)
    {
        CurFuel += (int)(_fuelMax * pct);
    }

    private void Tick()
    {
        CurFuel -= 1;
    }

    private int FuelWatcher(int newFuel)
    {
        if (newFuel < CurFuel) // lost
        {
            if (newFuel <= 0) // extinguishes
            {
                newFuel = 0;
                Debug.Log("RELEASE THE BEAST");
            }
        }
        else if (newFuel > CurFuel) // gained
        {
            newFuel = Mathf.Min(newFuel, _fuelMax);
        }
        else
            return newFuel;

        _lightObj.pointLightOuterRadius = _maxRadius * ((float)newFuel/_fuelMax);
        OnFuelChange?.Invoke((float)newFuel/_fuelMax);
        return newFuel;
    }
}
