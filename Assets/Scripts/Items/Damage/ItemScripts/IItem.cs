using System.Collections;
using System.Collections.Generic;
using GJam.Player;
using UnityEngine;

public interface IItem
{
    public SOItem Data {get;}
    public void Use_Main(PlayerController caller, Vector3 mouseDirVector);
    public void Use_Alt(PlayerController caller, Vector3 mouseDirVector);
}
