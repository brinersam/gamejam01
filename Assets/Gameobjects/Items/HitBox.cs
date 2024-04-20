using System.Collections;
using System.Collections.Generic;
using GJam.Items;
using GJam.Player;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] ItemwHitbox owner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out PlayerController plr)) //todo replace with generic Hittable component
            return;
        
        owner.OnHitting(plr, collision.gameObject.transform.position);
    }

}
