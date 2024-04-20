using GJam.Player;
using UnityEngine;

namespace GJam.Items
{
public class ItemwHitbox : MonoBehaviour
{
    [SerializeField] private SODamage _itemdata;
    [SerializeField] private GameObject _hitBoxObj;

    private int _coolDowncurr;

    private void OnEnable()
    {
        _hitBoxObj.SetActive(false);
    }

    public void Use_Main(PlayerController caller)
    {
        Debug.Log("l click");
    }

    public void Use_Alt(PlayerController caller)
    {
        Debug.Log("r click");
    }

    public void OnHitting(IHittable target, Vector3 targetPos)
    {
        Vector3 knockbackVector;
        if (_itemdata.Knockback <= 0)
            knockbackVector = Vector3.zero;
        else
            knockbackVector = Vector3.Normalize(targetPos - transform.position);
         
        Debug.DrawRay(transform.position, knockbackVector * 4, Color.red ,duration: 3);
        Debug.Log("drawing knockback ray");
        target.GetHitwKnockback(_itemdata, knockbackVector);
    }
}
}