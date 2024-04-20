using UnityEngine;
using UnityEngine.InputSystem;

using GJam.Living.Movement;
using GJam.Items;

namespace GJam.Player
{
    public class PlayerController : MonoBehaviour, IHittable
    {
        [SerializeField] private Movement _movement;
        [SerializeField] private ItemwHitbox _item;

        private void FixedUpdate()
        {
            transform.position += _movement.GetMovement() * Time.deltaTime;
        }

        public void SetClimbState(bool active)
        {
            _movement.SetIsClimbing(active);
        }

        private void OnJump(InputValue input)
        {
            _movement.JumpImpulse();
        }

        private void OnMovement(InputValue input)
        {
            _movement.ReceiveMovement(input.Get<Vector2>());
        }

        private void OnMainInteract(InputValue input)
        {
            if (_item is null)
            {
                Debug.Log("l click, no item");
                return;
            }
            _item.Use_Main(this);
            
        }

        private void OnAltInteract(InputValue input)
        {
            if (_item is null)
            {
                Debug.Log("r click, no item");
                return;
            }
            _item.Use_Alt(this);
        }

        public void GetHitwKnockback(SODamage _itemdata, Vector3 knockbackVector)
        {
            Debug.Log($"I was hit for {_itemdata.Damage} hp");
            _movement.ReceiveImpulse(knockbackVector * _itemdata.Knockback);
        }
    }
}