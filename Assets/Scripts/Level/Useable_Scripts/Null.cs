using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Null : MonoBehaviour, IUseable
{
    public void Use(out bool BlockUse)
    {
        BlockUse = false;
    }
}
