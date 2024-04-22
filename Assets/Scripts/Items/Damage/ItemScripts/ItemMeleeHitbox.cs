using System;
using GJam.Player;
using UnityEngine;

namespace GJam.Items
{
[Serializable]
public class ItemMeleeHitbox : IItem
    {
        [SerializeField] private SOItem _itemdata;
        [SerializeField] private HitBox _hitbox;
        private bool InCooldown = false;

        public SOItem Data => _itemdata;

        public ItemMeleeHitbox(SOItem data)
        {
            _itemdata = data;
        }
        
        public void Use_Main(PlayerController caller, Vector3 mouseDirVector)
        {
            if (InCooldown)
                return;

            InCooldown = true;
            System_Ticker.Instance.WaitCallback(_itemdata.AttackCooldown, () => InCooldown = false);

            if (!Validate())
                return;
            Vector3 callerPos = caller.transform.position;

            // Debug.Log("drawing attack ray");
            // Debug.DrawRay(callerPos, mouseDirVector * 2, Color.red, 2f);
            
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

        public void OnHitting(Health target, Vector3 knockbackVector) 
        {
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