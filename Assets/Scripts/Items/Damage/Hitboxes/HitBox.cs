using GJam.Items;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private ContactFilter2D _cf;
    private Collider2D _collider;
    public ItemMeleeHitbox _item;

    private void Awake()
    {   
        _collider = GetComponent<Collider2D>();
        gameObject.SetActive(false);
    }

    public void Flash()
    {
        gameObject.SetActive(true);
        Hit();
    }

    private void Hit()
    {
        Collider2D[] targetsArr = new Collider2D[10]; // max 10 targets
        _collider.OverlapCollider(_cf,targetsArr);

        foreach (Collider2D collision in targetsArr)
        {
            if (collision == null) //ran out of collisions
                break;

            // Debug.DrawLine(transform.position, collision.transform.position, Color.red, 4);
            // Debug.Log("drawing hit lines..");

            if (!collision.TryGetComponent(out Health target))
                continue;
        
            Vector3 knockbackVector = Vector3.zero;

            if (_item.Data.Knockback > 0)
                knockbackVector = Vector3.Normalize(collision.transform.position - transform.parent.position); // get vector from item's owner position to collision's object position
            
            _item.OnHitting(target, knockbackVector);
        }

        gameObject.SetActive(false);
    }
}
