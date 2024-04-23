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
    [Range(0,1)] public float Refiltorch_Pct;

    public bool HasHitbox => hitboxOBJ != null;
}
