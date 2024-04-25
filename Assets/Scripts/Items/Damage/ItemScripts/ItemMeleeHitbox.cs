using System;
using UnityEngine;

namespace GJam.Items
{
[Serializable]
public class ItemMeleeHitbox : IItem
    {
        // [SerializeField] private Animator _anims;

        [SerializeField] private SOItem _itemdata;
        [SerializeField] private HitBox _hitbox;
        private bool IsInCooldown = false;
        private bool IsAttackInProgress = false;

        public SOItem Data => _itemdata;

        public ItemMeleeHitbox(SOItem data)
        {
            _itemdata = data;
        }
        
        public void Use_Main(Transform caller, Vector3 mouseDirVector, Health hp = null, Torch trch = null, Animator attackAnims = null)
        {
            if (!Validate())
                return;

            if (IsInCooldown || IsAttackInProgress)
                return;

            IsAttackInProgress = true;
            //Debug.Log("Windup!....");
            if (attackAnims)
                attackAnims.SetTrigger("Windup");
                
            if (_itemdata.AttackWindUp <= 0)
                Attack(caller,mouseDirVector, attackAnims);
            else
                System_Ticker.Instance.WaitCallback(_itemdata.AttackWindUp, () => Attack(caller,mouseDirVector, attackAnims));
        }

        public void Use_Alt(Transform caller, Vector3 mouseDirVector)
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
            box._item = this;
        }

        private void Attack(Transform caller, Vector3 mouseDirVector, Animator attackAnims = null)
        {
            //Debug.Log("Attack!....");
            if (attackAnims)
                attackAnims.SetTrigger("Swing");

            IsInCooldown = true;
            System_Ticker.Instance.WaitCallback(_itemdata.AttackCooldown, () => {IsInCooldown = false; IsAttackInProgress = false;});

            // Debug.Log("drawing attack ray");
            // Debug.DrawRay(caller.position, mouseDirVector * 2, Color.red, 2f);
            
            _hitbox.transform.position = caller.position + mouseDirVector * _itemdata.AttackDistance;
            _hitbox.Flash(mouseDirVector.x < 0);
            //_hitbox.transform.Rotate(callerPos, Vector2.Angle(Vector2.right,mouseDirVector)); // todo
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
            return;
        }
    }
}