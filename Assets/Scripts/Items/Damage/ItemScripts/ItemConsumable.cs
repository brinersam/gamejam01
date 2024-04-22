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

    public void Use_Main(PlayerController caller, Vector3 mouseDirVector)
    {
        if (!Validate())
            return;

        caller.Health.GetHit(_itemdata, default);
        //inv.remove(this)
    }

    public void Use_Alt(PlayerController caller, Vector3 mouseDirVector)
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
