using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceTypeEnum
{
    Mitrhil,
    Resin,
}

public class System_Resources : MonoBehaviour
{
    private Dictionary<ResourceTypeEnum, int> _data;
    public static System_Resources Instance;


    private void Awake()
    {
        _data = new();
        Instance = this;
    }

    public void Resource_Receive(int amnt, ResourceTypeEnum type)
    {
        if (_data.ContainsKey(type) == false)
            _data[type] = 0;

        _data[type] += amnt;
    }

    public bool Resource_Spend(int amnt, ResourceTypeEnum type)
    {
        if (_data.ContainsKey(type) == false)
            _data[type] = 0;

        if (_data[type] - amnt <= 0)
            return false;

        _data[type] -= amnt;
        return true;
    }
    

}
