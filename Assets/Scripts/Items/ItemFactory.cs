using System.Collections;
using System.Collections.Generic;
using GJam.Items;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private SOItem Data;

    public ItemMeleeHitbox Get_ItemMeleeHitbox()
    {
        return new ItemMeleeHitbox(Data);
    }

}
