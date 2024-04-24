using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using GJam.Living.Movement;
using GJam.Items;
using System;

    [RequireComponent (typeof(Movement))]
    [RequireComponent (typeof(Health))]
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        [SerializeField] private GameObject _visualContainer;
        [SerializeField] private SpriteRenderer _visual;
        [SerializeField] private Transform _hitboxHolder;

        private bool _disableInput = false;

        private Movement _movement;
        private Health _health;
        private Torch _torch;
        private IItem[] _invArr = new IItem[4];
        private int _item_active_idx = 0;
        
        [SerializeField] private ItemSlot _item_slot1Visual;
        [SerializeField] private ItemSlot _item_slot2Visual;
        [SerializeField] private ItemSlot _item_slot3Visual;

        [SerializeField] private SOItem _startingItem_slot0;
        [SerializeField] private SOItem _startingItem_slot1;
        [SerializeField] private SOItem _startingItem_slot2;
        [SerializeField] private SOItem _startingItem_slot3;

        private IItem _item_slot0;
        private IItem _item_slot1HP;
        private IItem _item_slot2TORCH;
        private IItem _item_slot3;
        private IItem _activeItem => _invArr[_item_active_idx];

        public Health Health => _health;
        public Torch Torch => _torch;

        public Action ActivatableObjectsQueue;

        private void Awake()
        {
            Instance = this;

            if (TryGetComponent(out Torch trch))
                _torch = trch;
            else
                Debug.LogWarning("No torch component!", gameObject);

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
                _item_slot1HP = new ItemConsumable(_startingItem_slot1,_item_slot1Visual);
            if (_startingItem_slot2 != null)
                _item_slot2TORCH = new ItemConsumable(_startingItem_slot2,_item_slot2Visual);
            if (_startingItem_slot3 != null)
                _item_slot3 = new ItemConsumable(_startingItem_slot3,_item_slot3Visual);

            _invArr[0] = _item_slot0;
            _invArr[1] = _item_slot1HP;
            _invArr[2] = _item_slot2TORCH;
            _invArr[3] = _item_slot3;
        }

        private void Start()
        {
            SetActiveItem(_item_active_idx);
            Restore();
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

        public void Hide(bool on)
        {
            _movement.MovementDisabled = on;
            _visualContainer.SetActive(!on);
            _disableInput = on;
            ActivatableObjectsQueue = null;
        }

        public void Restore()
        {
            _health.Restore();
            _torch.Restore();
            _invArr[1].Restore(System_Playerconfig.Instance.Restore_HP());
            _invArr[2].Restore(System_Playerconfig.Instance.Restore_Torch());
        }

        private Vector3 CharacterToPointerNormalized()
        {
            return (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position).normalized;
        }


        private void OnJump(InputValue input)
        {
            if (_disableInput)
                return;
            _movement.JumpImpulse();
        }

        private void OnMovement(InputValue input)
        {
            if (_disableInput)
                return;
            Vector2 mvmnt = input.Get<Vector2>();
            _visual.flipX = mvmnt.x < 0;
            _movement.ReceiveMovement(mvmnt);
        }

        private void OnMainInteract(InputValue input)
        {
            if (_disableInput)
                return;
            if (_activeItem is null)
            {
                Debug.Log("l click, no item");
                return;
            }
            _activeItem.Use_Main(transform, CharacterToPointerNormalized(), _health, _torch);
        }

        private void OnAltInteract(InputValue input)
        {
            if (_disableInput)
                return;
            if (_activeItem is null)
            {
                Debug.Log("r click, no item");
                return;
            }
            _activeItem.Use_Alt(transform, CharacterToPointerNormalized());
        }

        private void OnInv_0(InputValue input)
        {
            if (_disableInput)
                return;
            SetActiveItem(0);
        }
        private void OnInv_1(InputValue input)
        {
            if (_disableInput)
                return;
            _invArr[1].Use_Main(transform,Vector3.zero,_health,_torch);
        }
        private void OnInv_2(InputValue input)
        {
            if (_disableInput)
                return;
            _invArr[2].Use_Main(transform,Vector3.zero,_health,_torch);
        }
        private void OnInv_3(InputValue input)
        {
            System_Teleporter.Instance.Teleport(TeleportType.Respawn);
            //_invArr[3].Use_Main(transform,Vector3.zero,_health,_torch);
        }
        private void OnUse(InputValue input)
        {
            if (_disableInput)
                return;
            ActivatableObjectsQueue?.Invoke();
        }
    }