using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour
{
    private bool _firstRestore = true;
    [SerializeField] private Light2D _lightObj;
    [SerializeField] private int _fuelMax = 120;
    private int __fuelCurrent;

    private float _maxRadius;
    private int CurFuel {get => __fuelCurrent; set {__fuelCurrent = FuelWatcher(value);}}
    
    public event Action<float> OnFuelChange;

    private void Awake()
    {
        _maxRadius = _lightObj.pointLightOuterRadius;
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
        if (_firstRestore)
        {
            _firstRestore = false;
            CurFuel = (int)(System_Playerconfig.Instance.Start_Torch * _fuelMax); 
            return;
        }
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
                if (System_GRU.Instance.IsSlumbers)
                    System_GRU.Instance.Gru_Awake();
            }
        }
        else if (newFuel > CurFuel) // gained
        {
            if (System_GRU.Instance.IsSlumbers == false)
                System_GRU.Instance.Gru_Sleep();
            newFuel = Mathf.Min(newFuel, _fuelMax);
        }
        else
            return newFuel;

        _lightObj.pointLightOuterRadius = _maxRadius * ((float)newFuel/_fuelMax);
        OnFuelChange?.Invoke((float)newFuel/_fuelMax);
        return newFuel;
    }
}
