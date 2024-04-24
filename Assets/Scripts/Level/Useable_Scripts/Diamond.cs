using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour, IUseable
{
    [SerializeField] private GameObject _diamondVisual;
    public void Use(out bool BlockUse)
    {
        BlockUse = true;
        _diamondVisual.SetActive(false);
        System_Flood.Instance.StartEndSequence();
    }
}
