using System.Collections;
using System.Collections.Generic;
using GJam.Player;
using UnityEngine;

public interface IItem
{
    public SOItem Data {get;}
    public void Use_Main(Transform callerPos, Vector3 mouseDirVector, Health hp = null, Torch trch = null);
    public void Use_Alt(Transform callerPos, Vector3 mouseDirVector);
}
