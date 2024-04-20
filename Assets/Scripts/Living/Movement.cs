using UnityEngine;

namespace GJam.Living.Movement
{
public class Movement : MonoBehaviour // todo coyote jumps and all that stuff  https://youtu.be/2S3g8CgBG1g?t=218
{
    [SerializeField] private Rigidbody2D _rBody;
    private float _rBodyGravityCached;

    private bool _isClimbing = false;
    private bool _isStunned = false;

    private const float MIN_COLLISION_DISTANCE = 0.1f;
    private Vector3 _internalColliderSize = Vector3.one * 0.9f;

    [SerializeField] private float _moveForce = 5;
    private Vector2 _mvntVector;
    
    [SerializeField] private float _jumpForce = 12;
    [Range(0.3f,1)] [SerializeField] private float _jumpDecayTick = 0.5f;
    private Vector2 _jumpVector = Vector2.up;
    private float _jumpForceCur = 0;

    private Vector3 _curImpulse;

    // [SerializeField] private float _gravityForce = 1;
    // private Vector2 _gravityVector = Vector2.down;

    private void Start()
    {
        _rBodyGravityCached = _rBody.gravityScale;
    }

    public Vector3 GetMovement()
    {
        Vector3 movementDir = Vector3.zero;

        movementDir += TryAddVector(_mvntVector, _moveForce);
        //movementDir += TryAddVector(_gravityVector, _gravityForce); // best for rbody to handle gravity
        movementDir += TryAddVector(_jumpVector, _jumpForceCur);

        if (_jumpForceCur > 0)
            _jumpForceCur -= _jumpDecayTick;

        return movementDir;
    }

    public void JumpImpulse()
    {
        if (_jumpForceCur > 0) // prevent jumping while midair from jumping (double jump before fall real??) - use state to get isMidAir from
            return;

        if (_isClimbing)
        {
            _jumpForceCur = _jumpForce * 0.5f;
            return;
        }

        _jumpForceCur = _jumpForce; // + _gravityForce;
    }

    public void ReceiveImpulse(Vector3 impulse)
    {
        _rBody.AddForce(impulse);
    }

    public void ReceiveMovement(Vector2 mvmnt)
    {
        _mvntVector = ProcessMovement(mvmnt);
    }

    public void SetIsClimbing(bool active)
    {
        if (active)
        {
            _rBody.gravityScale = 0;
            _rBody.velocity = Vector2.zero;
            _isClimbing = true;
        }
        else
        {
            _rBody.gravityScale = _rBodyGravityCached;
            _isClimbing = false;
        }
    }

    private Vector2 ProcessMovement(Vector2 mvmnt)
    {
        if (_isClimbing)
        {
            return mvmnt;
        }
        if (_isStunned)
        {
            return Vector2.zero;
        }
        else
        {
            return new Vector2(mvmnt.x,0);
        }
    }

    private Vector3 TryAddVector(Vector3 movementDirection, float force)
    {
        if (force <= 0)
            return Vector3.zero;

        var collision = Collision(movementDirection,force);
        if (collision == true)
        {
            if (collision.distance <= MIN_COLLISION_DISTANCE) // movement blocked
                return Vector3.zero;
            
            if (collision.distance < .2f && force > collision.distance) // pillow to prevent getting stuck // activates at distance of .15
                force = 0.1f;
        }

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
                LayerMask.GetMask("Default"));
    }
}
}