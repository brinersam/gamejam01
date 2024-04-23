using System.Collections;
using GJam.Items;
using GJam.Player;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] bool _playerOwned = false;

    private Collider2D _collider;
    private ContactFilter2D _cf; // literally does not work (wow unity api not working again, shocker!!)

    public ItemMeleeHitbox owner;


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();

        // ContactFilter2D _cf = new();//{ useTriggers = true};
        // //_cf.SetLayerMask(LayerMask.GetMask(LayerMask.LayerToName(3)));
        // //_cf.SetLayerMask(Physics2D.GetLayerCollisionMask(6));

        // _cf.SetLayerMask(default);
        // //_cf.SetLayerMask(LayerMask.GetMask(LayerMask.LayerToName(i)));
        // Debug.Log($"default :: Returns true when player is excluded by the filter: {_cf.IsFilteringLayerMask(PlayerController.Instance.gameObject)}");

        // for (int i = 0; i < 16; i++)
        // {
        //     _cf.SetLayerMask(Physics2D.GetLayerCollisionMask(i));
        //     //_cf.SetLayerMask(LayerMask.GetMask(LayerMask.LayerToName(i)));
        //     Debug.Log($"i:{i} :: Returns true when player is excluded by the filter: {_cf.IsFilteringLayerMask(PlayerController.Instance.gameObject)}");
        //     _cf.SetLayerMask(default);
        // }
        
        gameObject.SetActive(false);
    }

    public void Flash()
    {
        gameObject.SetActive(true);
        Hit();
        //StartCoroutine(nameof(UnFlash));
    }

    private void Hit()
    {
        Debug.Log("hit");
        Collider2D[] targetsArr = new Collider2D[10]; // max 10 targets
        _collider.OverlapCollider(_cf,targetsArr);

        foreach (Collider2D collision in targetsArr)
        {
            if (collision == null) //ran out of collisions
                break;

            if (collision.gameObject.layer == 3 && _playerOwned) // dont kill urself as a player
                continue;

            if (!collision.TryGetComponent(out Health target))
                continue;
            
            // Debug.DrawLine(transform.position, collision.transform.position, Color.red, 4);
            // Debug.Log("drawing hit lines..");
        
            Vector3 knockbackVector = Vector3.zero;

            if (owner.Data.Knockback > 0)
                knockbackVector = Vector3.Normalize(collision.transform.position - transform.position); // get vector from item's owner position to collision's object position
            
            owner.OnHitting(target, knockbackVector);
        }

        gameObject.SetActive(false);
    }

    // private void OnTriggerStay2D(Collider2D collision)
    // {   
    //     if (!collision.TryGetComponent(out Health target))
    //         return;
        
    //     Vector3 knockbackVector = Vector3.zero;

    //     if (owner.Data.Knockback > 0)
    //         knockbackVector = Vector3.Normalize(collision.transform.position - transform.position); // get vector from item's owner position to collision's object position
        
    //     // Debug.DrawRay(transform.position, knockbackVector * 4, Color.red ,duration: 3);
    //     // Debug.Log("drawing knockback ray");

    //     owner.OnHitting(target, knockbackVector);
    //     gameObject.SetActive(false);
    // }

    // IEnumerator UnFlash()
    // {
    //     yield return _lifeTime;
    //     gameObject.SetActive(false);
    // }
}
