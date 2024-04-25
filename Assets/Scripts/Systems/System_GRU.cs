using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_GRU : MonoBehaviour
{public static System_GRU Instance;

    [SerializeField] private Animator _visual;
    WaitForSeconds Waiter = new(2);

    public bool IsSlumbers {get;private set;} = true;

    private void Awake()
    {
        Instance = this;
    }
    public void Gru_Awake()
    {
        IsSlumbers = false;
        _visual.SetTrigger("WakeUp");
        System_Ticker.Instance.WaitCallback(1.5f, StartAwakening);
    }

    public void Gru_Sleep()
    {
        IsSlumbers = true;
        _visual.SetBool("IsAwake", false);
    }

    private void StartAwakening()
    {
        if (IsSlumbers)
            return;

        _visual.SetBool("IsAwake", true);
        StartCoroutine(nameof(HitPlayer));
    }

    IEnumerator HitPlayer()
    {
        yield return Waiter;

        if (IsSlumbers)
            yield break;

        _visual.SetTrigger("Hit");
        PlayerController.Instance.Health.CurHp -= 3;
        StartCoroutine(nameof(HitPlayer));
    }


}
