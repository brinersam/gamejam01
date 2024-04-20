using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SODamage", menuName = "SObjects/Weapons/SODamage" )]
public class SODamage : ScriptableObject
{
    public int Damage;
    public int Knockback;
    public int AttackSpeed;
}
