using UnityEngine;

[CreateAssetMenu(fileName = "SOItem", menuName = "SObjects/SOItem" )]
public class SOItem : ScriptableObject
{
    public int Damage;
    public int Knockback;
    public float AttackCooldown;
    public int AttackDistance;
    public GameObject hitboxOBJ;
    public bool SelfDamage;

    public bool HasHitbox => hitboxOBJ != null;
}
