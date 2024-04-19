using UnityEngine;
using UnityEngine.InputSystem;


namespace GJam.Player
{
    public class PlayerController : MonoBehaviour
    {

        private Vector3 _colliderSize = Vector3.one * 0.9f;

    #region Movement
        [SerializeField] private float _moveForce = 2;
        private Vector2 _mvntVector;
        
        [SerializeField] private float _jumpForce = 1;
        [Range(0,1)] [SerializeField] private float _jumpDecayTick = 0.1f;
        private Vector2 _jumpVector = Vector2.up;
        private float _jumpForceCur = 0;

        [SerializeField] private float _gravityForce = 1;
        private Vector2 _gravityVector = Vector2.down;
    #endregion

        private void Update()
        {
            ApplyForces();
        }

        private void ApplyForces()
        {
            if (_jumpForceCur > 0)
                _jumpForceCur -= _jumpDecayTick;

            Vector3 movementDir = Vector3.zero;

            movementDir += TryAddVector(_mvntVector, _moveForce);
            movementDir += TryAddVector(_gravityVector, _gravityForce);
            movementDir += TryAddVector(_jumpVector, _jumpForceCur);

            transform.position += Time.deltaTime * movementDir;
        }

        private Vector3 TryAddVector(Vector3 movementDirection, float force)
        {
            if (MvmntBlocked(movementDirection,force) || force <= 0)
                return Vector3.zero;

            // todo
            // step (force * mvmntDir) is too big sometimes so we "walk" into collider and get stuck
            // limit force here if far too deep into collider

            return force * movementDirection;
        }

        private bool MvmntBlocked(Vector3 movementDirection, float force)
        {
            // if force is above "safe number" and clips then cast again using less force and if that succeeds, modify the force
            return Physics2D.BoxCast(
                        transform.position,
                        _colliderSize,
                        0f,
                        movementDirection,
                        0.1f); //Math.Max(0.01f, force); 
        }

        private void OnJump(InputValue input)
        {
            if (_jumpForceCur > 0) // prevent jumping while midair from jumping (double jump before fall real??)
                return;

            _jumpForceCur = _jumpForce + _gravityForce;
        }

        private void OnMovement(InputValue input)
        {
            _mvntVector = input.Get<Vector2>();
        }
    }
}