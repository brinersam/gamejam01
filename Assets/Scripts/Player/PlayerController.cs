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

        private Movement _movement;
        private Health _health;
        [SerializeField] private Transform _hitboxHolder;

        private IItem[] _invArr = new IItem[4];
        private int _item_active_idx = 0;
        
        [SerializeField] private SOItem _startingItem_slot0;
        [SerializeField] private SOItem _startingItem_slot1;
        [SerializeField] private SOItem _startingItem_slot2;
        [SerializeField] private SOItem _startingItem_slot3;

        [SerializeField] private IItem _item_slot0;
        [SerializeField] private IItem _item_slot1;
        [SerializeField] private IItem _item_slot2;
        [SerializeField] private IItem _item_slot3;
        private IItem _activeItem => _invArr[_item_active_idx];

        public Health Health => _health;

        private void Awake()
        {
            Instance = this;

            if (TryGetComponent(out Health hp))
                _health = hp;
            else
                Debug.LogWarning("No health component!", gameObject);

            if (TryGetComponent(out Movement mvnt))
                _movement = mvnt;
            else
                Debug.LogWarning("No movement component!", gameObject);

            if (_startingItem_slot0 != null)
                _item_slot0 = new ItemMeleeHitbox(_startingItem_slot0);
            if (_startingItem_slot1 != null)
                _item_slot1 = new ItemConsumable(_startingItem_slot1);
            if (_startingItem_slot2 != null)
                _item_slot2 = new ItemConsumable(_startingItem_slot2);
            if (_startingItem_slot3 != null)
                _item_slot3 = new ItemConsumable(_startingItem_slot3);

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

        public void SetActiveItem(int slotIdx)
        {
            _item_active_idx = slotIdx;
            
            UpdateHitbox(_activeItem as ItemMeleeHitbox);
        }

        public void UpdateHitbox(ItemMeleeHitbox item)
        {
            foreach (Transform child in _hitboxHolder)
                Destroy(child.gameObject);

            if (item is null || item.Data.HasHitbox == false)
                return;
            
            item.LinkHitbox(Instantiate(item.Data.hitboxOBJ, _hitboxHolder, false)
                                .GetComponent<HitBox>());
        }

        private Vector3 CharacterToPointerNormalized()
        {
            return ((Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position)*4).normalized;
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

        private void OnInv_0(InputValue input)
        {
            SetActiveItem(0);
        }
        private void OnInv_1(InputValue input)
        {
            SetActiveItem(1);
        }
        private void OnInv_2(InputValue input)
        {
            SetActiveItem(2);
        }
        private void OnInv_3(InputValue input)
        {
            SetActiveItem(3);
        }
    }
}