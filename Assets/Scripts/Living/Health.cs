using System;
using GJam.Living.Movement;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private bool _isPlayer = false;
    [SerializeField] private int _maxHp = 2;

    private Movement _movement;
    private Rigidbody2D _rBody;
    private bool _healthGated = false;
    
    private int __curHp;
    public int CurHp {get {return __curHp;} set {__curHp = HPWatcher(value);}}

    public event Action OnDeath;

    private void Awake()
    {
        if (TryGetComponent(out Movement movement))
            _movement = movement;
        else if (TryGetComponent(out Rigidbody2D rbody)) // cat is fine too
            _rBody = rbody;

        __curHp = _maxHp;
    }

    public void GetHit(SOItem _itemdata, Vector3 knockbackVector)
    {
        CurHp -= _itemdata.Damage;

        if (_movement != null)
            _movement.ReceiveImpulse(knockbackVector * _itemdata.Knockback);
        else if (_rBody != null)
            _rBody.AddForce(knockbackVector * _itemdata.Knockback);
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Debug.Log("death...");
        
        if (_isPlayer)
            Debug.Log("...of the player...");
        
        gameObject.SetActive(false);
    }

    private int HPWatcher(int newHp)
    {
        if (newHp < CurHp) // damaged
        {
            if (newHp <= 0) // dead if we apply this damage
            {
                if ( _isPlayer && _healthGated == false) //healthgate, player only
                {
                    _healthGated = true;
                    return 1; // ignore damage set hp to minimal
                }
                
                Die();
                return newHp;
            }
        }
        else if (newHp > CurHp) // healed
        {
            if (_healthGated == true) // reset healthgate
                _healthGated = false;
        }

        return newHp;
    }

}
