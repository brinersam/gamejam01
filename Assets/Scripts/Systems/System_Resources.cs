using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Resources : MonoBehaviour
{
    public static System_Resources Instance;

    public int Mithril_Collected = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void ReceiveMithril(int amnt) //Resourceenum type // todo if more resources (maybe lamp oil will be here lol)
    {
        Mithril_Collected += amnt;
        Debug.Log($"current mithril: {Mithril_Collected}");
    }
    

}
