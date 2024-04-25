using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public SOItem Data {get;}
    public void Restore(int amnt);
    public void Use_Main(Transform callerPos, Vector3 mouseDirVector, Health hp = null, Torch trch = null, Animator attackAnims = null);
    public void Use_Alt(Transform callerPos, Vector3 mouseDirVector);
}
