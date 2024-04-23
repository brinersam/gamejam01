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
        InvokeRepeating(nameof(Tick),1f,1f);
    }

    private void Tick()
    {
        OnSecond?.Invoke();
    }

    public void WaitCallback(float seconds, Action callback) // coroutines from within non monobeh scripts!
    {
        StartCoroutine(nameof(Wildcard),(seconds,callback));
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
