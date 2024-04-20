using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    void GetHitwKnockback(SODamage damageData, Vector3 knockbackVector);
}
