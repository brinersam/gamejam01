using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LootDrop : MonoBehaviour
{
    [SerializeField] private int MithrilDropAmnt = 0;

    public void Drop()
    {
        System_Resources.Instance.ReceiveMithril(MithrilDropAmnt);
    }

}
