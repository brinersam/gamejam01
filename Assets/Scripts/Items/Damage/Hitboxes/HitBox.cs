using System.Collections;
using System.Collections.Generic;
using GJam.Items;
using GJam.Player;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public ItemMeleeHitbox owner;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Flash()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   
        if (!collision.TryGetComponent(out Health target))
            return;
        
        Vector3 knockbackVector = Vector3.zero;

        if (owner.Data.Knockback > 0)
            knockbackVector = Vector3.Normalize(collision.transform.position - transform.position); // get vector from item's owner position to collision's object position
        
        Debug.DrawRay(transform.position, knockbackVector * 4, Color.red ,duration: 3);
        Debug.Log("drawing knockback ray");

        owner.OnHitting(target, knockbackVector);
        gameObject.SetActive(false);
    }
}
