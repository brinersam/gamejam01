using System;
using GJam.Player;
using UnityEngine;

namespace GJam.Items
{
[Serializable]
public class Item
{
    [SerializeField] private SOItem _itemdata;
    [SerializeField] private HitBox _hitbox;
    private bool _playerOwned;
    public SOItem Data => _itemdata;

    public Item(SOItem data, bool heldByPlayer = false)
    {
        _playerOwned = heldByPlayer;   
        _itemdata = data;
    }
    
    public void Use_Main(PlayerController caller, Vector3 mouseDirVector)
    {
        if (!Validate())
            return;
        Vector3 callerPos = caller.transform.position;

        Debug.Log("drawing attack ray");
        Debug.DrawRay(callerPos, mouseDirVector * 2, Color.red, 2f);
        
        _hitbox.transform.position = callerPos + mouseDirVector * _itemdata.AttackDistance;
        _hitbox.Flash();
        //_hitbox.transform.Rotate(callerPos, Vector2.Angle(Vector2.right,mouseDirVector)); // todo
    }

    public void Use_Alt(PlayerController caller, Vector3 mouseDirVector)
    {
        if (!Validate())
            return;

        Debug.Log("r click");
    }

    public void OnHitting(PlayerController target, Vector3 knockbackVector) //todo replace with generic Hittable component
    {
        if (target.gameObject == PlayerController.Instance.gameObject && _playerOwned && _itemdata.SelfDamage == false)
        {
            return; // not really necessary i think unless we have bombs
        }

        target.GetHit(_itemdata, knockbackVector);
    }

    public void LinkHitbox(HitBox box)
    {
        this._hitbox = box;
        box.owner = this;
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