using UnityEngine;
using UnityEngine.InputSystem;

namespace GJam.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        private void FixedUpdate()
        {
            transform.position += _playerMovement.GetMovement() * Time.deltaTime;
        }

        public void SetClimbState(bool active)
        {
            _playerMovement.SetIsClimbing(active);
        }


        private void OnJump(InputValue input)
        {
            _playerMovement.JumpImpulse();
        }

        private void OnMovement(InputValue input)
        {
            _playerMovement.ReceiveMovement(input.Get<Vector2>());
        }
    }
}