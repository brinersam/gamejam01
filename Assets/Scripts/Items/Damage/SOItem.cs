using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOItem", menuName = "SObjects/SOItem" )]
public class SOItem : ScriptableObject
{
    public int Damage;
    public int Knockback;
    public int AttackSpeed;
    public GameObject hitboxOBJ;

    public bool HasHitbox => hitboxOBJ != null;
}
