using System;
using System.Collections;
using System.Collections.Generic;
using GJam.Player;
using UnityEngine;

public class ItemConsumable : IItem
{
    [SerializeField] private SOItem _itemdata;
    private ItemSlot _visual;
    private int _amount;

    public SOItem Data => _itemdata;
    

    public ItemConsumable(SOItem data, ItemSlot visual) // Inventory inv
    {
        _itemdata = data;
        _visual = visual;
    }

    public void Use_Main(Transform callerPos, Vector3 mouseDirVector, Health hp = null, Torch trch = null)
    {
        if (_amount <= 0)
            return;

        _visual.UpdateVisual(--_amount);

        if (!Validate())
            return;

        if (hp)
            hp.GetHit(_itemdata, default);
        
        if (trch)
            trch.RefillPct(_itemdata.Refiltorch_Pct);
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

    public void Restore(int amnt)
    {
        _amount = Mathf.Max(amnt,_amount);
        _visual.UpdateVisual(_amount);
    }
}
