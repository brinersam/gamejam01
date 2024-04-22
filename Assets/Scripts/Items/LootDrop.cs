using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LootDrop : MonoBehaviour
{
    [SerializeField] private int MithrilDropAmnt = 0;

    private void Start()
    {
        System_Ticker.Instance.OnSecond += () => Debug.Log("bruh");
    }

    public void Drop()
    {
        System_Resources.Instance.ReceiveMithril(MithrilDropAmnt);
    }

}
