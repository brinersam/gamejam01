using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class Useable : MonoBehaviour // when approached, adds itself to player interaction field which is actiaved by pressing "E"
{
    [SerializeField] private GameObject _selector; // ideally instead of this, we'd use pixel outline shadergraph 
    // but that will take at least an hour of figuring out // todo

    [SerializeField] private MonoBehaviour _IUseableMonoBeh;
    private bool _subscribed = false;

    private void Awake()
    {
        _selector.SetActive(false);

        if ((_IUseableMonoBeh as IUseable) == null)
            Debug.LogError("FORGOT TO ADD PROPER SCRIPT TO USEABLE @", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) // activated by player only
    {
        Subscribe();
        _selector.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision) // activated by player only
    {
        Unsubscribe();
        _selector.SetActive(false);
    }

    private void Subscribe()
    {
        PlayerController.Instance.ActivatableObjectsQueue += Callback;
        _subscribed = true;
    }

    private void Unsubscribe()
    {
        if (_subscribed)
        {
            _subscribed = false;
            PlayerController.Instance.ActivatableObjectsQueue -= Callback;
        }
    }

    private void Callback()
    {
        Debug.Log("used a thing");

        (_IUseableMonoBeh as IUseable).Use(out bool BlockFurtherUse);

        if (BlockFurtherUse)
            DisableUsage();
    }
    
    private void DisableUsage()
    {
        GetComponent<Collider2D>().enabled = false;
        Unsubscribe();
        _selector.SetActive(false);
    }

}
