using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    void GetHit(SOItem damageData, Vector3 knockbackVector);
}
