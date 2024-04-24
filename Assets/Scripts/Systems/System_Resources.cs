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
    [SerializeField] private ResourceDisplayer _mithril;
    [SerializeField] private ResourceDisplayer _resin;
    private Dictionary<ResourceTypeEnum, ResourceDisplayer> _dataVisual;
    private Dictionary<ResourceTypeEnum, int> _data;
    public static System_Resources Instance;

    private void Awake()
    {
        _data = new();
        _dataVisual = new();

        _dataVisual[ResourceTypeEnum.Mitrhil] = _mithril;
        _dataVisual[ResourceTypeEnum.Resin] = _resin;

        Instance = this;
    }

    public void Resource_Receive(int amnt, ResourceTypeEnum type)
    {
        if (_data.ContainsKey(type) == false)
            _data[type] = 0;

        _data[type] += amnt;
        UpdateVisuals(type);
    }

    public bool Resource_Spend(int amnt, ResourceTypeEnum type)
    {
        if (_data.ContainsKey(type) == false)
            _data[type] = 0;

        if (_data[type] - amnt < 0)
            return false;

        _data[type] -= amnt;
        UpdateVisuals(type);
        return true;
    }

    private void UpdateVisuals(ResourceTypeEnum type)
    {
        _dataVisual[type].UpdateVisuals(_data[type]);
    }
    

}
