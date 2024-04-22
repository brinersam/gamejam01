using System;
using GJam.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GJam.Items
{
[Serializable]
public class Item
{
    [SerializeField] private SOItem _itemdata;
    public SOItem Data => _itemdata;

    public Item(SOItem data)
    {
        _itemdata = data;
    }
    
    public void Use_Main(PlayerController caller, Vector3 mouseDirVector)
    {
        if (!Validate())
            return;

        Debug.DrawRay(caller.transform.position, mouseDirVector * 2, Color.red, 2f);
        Debug.Log("l click");
    }

    public void Use_Alt(PlayerController caller, Vector3 mouseDirVector)
    {
        if (!Validate())
            return;

        Debug.Log("r click");
    }

    public void OnHitting(IHittable target, Vector3 knockbackVector)
    {
        // Vector3 knockbackVector;
        // if (_itemdata.Knockback <= 0)
        //     knockbackVector = Vector3.zero;
        // else
        //     knockbackVector = Vector3.Normalize(targetPos - transform.position);
         
        target.GetHit(_itemdata, knockbackVector);
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
}