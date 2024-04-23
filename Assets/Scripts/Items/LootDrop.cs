using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LootDrop : MonoBehaviour
{
    [SerializeField] private int MithrilDropAmnt = 0;
    [SerializeField] private int ResinDropAmnt = 0;

    public void Drop()
    {
        System_Resources.Instance.Resource_Receive(MithrilDropAmnt, ResourceTypeEnum.Mitrhil);
        System_Resources.Instance.Resource_Receive(ResinDropAmnt, ResourceTypeEnum.Resin);
    }

}
