using System;
using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class RefillStation : MonoBehaviour, IUseable
{
    [SerializeField] private Animator _anims;
    [SerializeField] bool _forceWorking = false;
    [SerializeField] private RefillStation _prevStation;
    [SerializeField] private Leak[] _leaksSinceLastStation;
    [SerializeField] bool _debugRays = false;

    public bool IsWorking => IsWorkingCached();

    private void Start()
    {
        if (!_forceWorking)
        {
            if (_prevStation == null)
                Debug.LogWarning("Previous station not linked @", gameObject);

            if (_leaksSinceLastStation.Length == 0)
                Debug.LogWarning("No leaks registered @", gameObject);
        }

        if (_debugRays)
            DrawDebug();
    }

    public void Use(out bool BlockFurtherUses)
    {
        BlockFurtherUses = false;
        if (IsWorking == false)
        {
            Debug.Log("doesnt work");
            return;
        }
            
        Restock();
        ToggleShop();
    }

    private bool IsWorkingCached()
    {
        if (_forceWorking || _prevStation.IsWorking && LeaksExist() == false)
        {
            _anims.SetBool("Fixed", true);
            _forceWorking = true;
            return true;
        }
        return false;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        CloseShop();
    }

    private void ToggleShop()
    {
        System_UI.Instance.EnableShop(true, true);
    }
    private void CloseShop()
    {
        System_UI.Instance.EnableShop(false);
        Restock();
    }

    private void Restock()
    {
        if (IsWorking == false)
        {
            Debug.Log("resotck failed => doesnt work");
            return;
        }
        PlayerController.Instance.Restore();
    }

    private bool LeaksExist()
    {
        for (int idx = 0; idx < _leaksSinceLastStation.Length; idx++)
        {
            if (_leaksSinceLastStation[idx].Fixed == false)
                return true;
        }
        return false;
    }
    
    private void DrawDebug()
    {
        Debug.DrawLine(
            transform.position,
            _prevStation.transform.position,
            Color.red,
            20);

        for (int idx = 0; idx < _leaksSinceLastStation.Length; idx++)
        {
            Debug.DrawLine(
                transform.position,
                _leaksSinceLastStation[idx].transform.position,
                Color.magenta,
                20);
        }
    }
}
