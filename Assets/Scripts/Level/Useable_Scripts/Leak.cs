using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leak : MonoBehaviour, IUseable
{
    [SerializeField] private ParticleSystem _particleSystem;
    readonly private int _resinToFix = 20;
    public bool Fixed {get;private set;}

    public void Use(out bool BlockUse)
    {
        BlockUse = false;
        if (System_Resources.Instance.Resource_Spend(_resinToFix, ResourceTypeEnum.Resin))
        {
            BlockUse = true;
            Fixed = true;
            GetFixed();
            _particleSystem.Stop();
        }
    }

    private void GetFixed()
    {
        Debug.Log("I am fixed!");
    }
}
