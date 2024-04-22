using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using GJam.Living.Movement;
using GJam.Items;


namespace GJam.Player
{
    [RequireComponent (typeof(Movement))]
    [RequireComponent (typeof(Health))]
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        [SerializeField] private Movement _movement;
        [SerializeField] private Transform _hitboxHolder;

        private Item[] _invArr = new Item[4];
        private int _item_active_idx = 0;
        
        [SerializeField] private SOItem _startingItem_slot0;
        [SerializeField] private SOItem _startingItem_slot1;
        [SerializeField] private SOItem _startingItem_slot2;
        [SerializeField] private SOItem _startingItem_slot3;

        [SerializeField] private Item _item_slot0;
        [SerializeField] private Item _item_slot1;
        [SerializeField] private Item _item_slot2;
        [SerializeField] private Item _item_slot3;
        private Item _activeItem => _invArr[_item_active_idx];

        private void Awake()
        {
            Instance = this;

            if (_startingItem_slot0 != null)
                _item_slot0 = new Item(_startingItem_slot0, true);
            if (_startingItem_slot1 != null)
                _item_slot1 = new Item(_startingItem_slot1, true);
            if (_startingItem_slot2 != null)
                _item_slot2 = new Item(_startingItem_slot2, true);
            if (_startingItem_slot3 != null)
                _item_slot3 = new Item(_startingItem_slot3, true);

            _invArr[0] = _item_slot0;
            _invArr[1] = _item_slot1;
            _invArr[2] = _item_slot2;
            _invArr[3] = _item_slot3;
        }

        private void Start()
        {
            SetActiveItem(_item_active_idx);
        }

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
            if (_activeItem is null)
            {
                Debug.Log("l click, no item");
                return;
            }
            _activeItem.Use_Main(this, CharacterToPointerNormalized());
        }

        private void OnAltInteract(InputValue input)
        {
            if (_activeItem is null)
            {
                Debug.Log("r click, no item");
                return;
            }
            _activeItem.Use_Alt(this, CharacterToPointerNormalized());
        }

        public void SetActiveItem(int slotIdx)
        {
            _item_active_idx = slotIdx;
            UpdateHitbox(_activeItem);
        }

        public void UpdateHitbox(Item item)
        {
            foreach (GameObject child in _hitboxHolder)
                Destroy(child);

            if (item == null || item.Data.HasHitbox == false)
                return;
            
            item.LinkHitbox(Instantiate(item.Data.hitboxOBJ, _hitboxHolder, false)
                                .GetComponent<HitBox>());
        }

        private Vector3 CharacterToPointerNormalized()
        {
            return ((Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position)*2).normalized;
        }
    }
}