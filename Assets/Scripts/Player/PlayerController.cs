using UnityEngine;
using UnityEngine.InputSystem;


namespace GJam.Player
{
    public class PlayerController : MonoBehaviour
    {
    #region Collision
        [SerializeField] private Rigidbody2D _rBody;
        [SerializeField] private Collider2D _collider;
        private const float MIN_COLLISION_DISTANCE = 0.1f;
        private Vector3 _internalColliderSize = Vector3.one * 0.9f;
    #endregion

    #region Movement
        [SerializeField] private float _moveForce = 2;
        private Vector2 _mvntVector;
        
        [SerializeField] private float _jumpForce = 1;
        [Range(0.3f,1)] [SerializeField] private float _jumpDecayTick = 0.1f;
        private Vector2 _jumpVector = Vector2.up;
        private float _jumpForceCur = 0;

        [SerializeField] private float _gravityForce = 1;
        private Vector2 _gravityVector = Vector2.down;
    #endregion

        private void FixedUpdate()
        {
            ApplyMvmntForces();
        }

        private void ApplyMvmntForces()
        {
            Vector3 movementDir = Vector3.zero;

            _mvntVector = new Vector2(_mvntVector.x,0);

            movementDir += TryAddVector(_mvntVector, _moveForce);
            movementDir += TryAddVector(_gravityVector, _gravityForce);
            movementDir += TryAddVector(_jumpVector, _jumpForceCur);

            transform.position += movementDir * Time.deltaTime;
            Debug.Log(movementDir);

            if (_jumpForceCur > 0)
                _jumpForceCur -= _jumpDecayTick;
        }

        private Vector3 TryAddVector(Vector3 movementDirection, float force)
        {
            if (force <= 0)
                return Vector3.zero;

            // var collision = Collision(movementDirection,force);

            // if (collision == true)
            // {
            //     if (collision.distance <= MIN_COLLISION_DISTANCE) // movement blocked
            //         return Vector3.zero;
                
            //     if (collision.distance < .2f && force > collision.distance) // pillow to prevent getting stuck // activates at distance of .15
            //         force = 0.1f;
            // }

            return (force - 0.1f) * movementDirection ;
        }

        private RaycastHit2D Collision(Vector3 movementDirection, float force)
        {
            return Physics2D.BoxCast(
                    transform.position,
                    _internalColliderSize,
                    0f,
                    movementDirection,
                    force,
                    layerMask: 0
                    );
        }

        private void OnJump(InputValue input)
        {
            if (_jumpForceCur > 0) // prevent jumping while midair from jumping (double jump before fall real??) - use state to get isMidAir from
                return;

            //transform.position += Vector3.up * 0.1f; // in case of stuck jump out of it

            _jumpForceCur = _jumpForce + _gravityForce;
        }

        private void OnMovement(InputValue input)
        {
            _mvntVector = input.Get<Vector2>();
        }
    }
}