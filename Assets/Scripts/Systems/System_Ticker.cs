using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Ticker : MonoBehaviour
{
    public static System_Ticker Instance;
    public event Action OnSecond;

    WaitForSeconds waitSeconds_1 = new WaitForSeconds(1f);

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(nameof(TimerLoop));
    }

    public void WaitCallback(float seconds, Action callback)
    {
        StartCoroutine(nameof(Wildcard),(seconds,callback));
    }


    IEnumerator TimerLoop()
    {
        yield return waitSeconds_1;
        OnSecond?.Invoke();
    }

    IEnumerator Wildcard((float,Action) tuple)
    {
        yield return new WaitForSeconds(tuple.Item1);
        tuple.Item2.Invoke();
    }

    // private void Update()
    // {
    //     _secCldwn += Time.deltaTime;

    //     if (_secCldwn >= 1)
    //     {
    //         _secCldwn = 0;
    //         OnSecond?.Invoke();
    //     }
            
    // }
}
