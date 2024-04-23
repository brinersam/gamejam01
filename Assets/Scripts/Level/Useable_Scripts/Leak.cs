using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leak : MonoBehaviour, IUseable
{
    public void Use(out bool BlockUse)
    {
        Debug.Log("i am fixed!", gameObject);
        BlockUse = true;
    }

}
