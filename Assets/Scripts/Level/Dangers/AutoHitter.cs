using System.Collections;
using System.Collections.Generic;
using GJam.Items;
using UnityEngine;

public class AutoHitter : MonoBehaviour
{
    [SerializeField] Transform _hitboxHolder;
    [SerializeField] SOItem _weaponData;
    [SerializeField] ItemMeleeHitbox _weapon;

    void Start()
    {
        _weapon = new ItemMeleeHitbox(_weaponData);
        UpdateHitbox(_weapon);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Attack(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Attack(other);
    }

    private void Attack(Collider2D other)
    {
        _weapon.Use_Main(
            transform,
            (other.transform.position - transform.position).normalized);
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
}
