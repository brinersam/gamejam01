using System;
using System.Collections;
using System.Collections.Generic;
using GJam.Player;
using UnityEngine;

public class ItemConsumable : IItem
{
    [SerializeField] private SOItem _itemdata;
    private bool disposedValue;

    public SOItem Data => _itemdata;
    

    public ItemConsumable(SOItem data) // Inventory inv
    {
        _itemdata = data;
    }

    public void Use_Main(Transform callerPos, Vector3 mouseDirVector, Health hp = null, Torch trch = null)
    {
        if (!Validate())
            return;

        if (hp)
            hp.GetHit(_itemdata, default);
        
        if (trch)
            trch.RefillPct(_itemdata.Refiltorch_Pct);
        //inv.remove(this)
    }

    public void Use_Alt(Transform callerPos, Vector3 mouseDirVector)
    {
        if (!Validate())
            return;

        return;
    }

    private bool Validate()
    {
        if (_itemdata == null)
        {
            Debug.LogError("Item has no data!!");
            return false;
        }
        return true;
    }
}
