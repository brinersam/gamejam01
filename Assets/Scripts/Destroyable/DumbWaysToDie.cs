using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DumbWaysToDieEnum
{
    Destroy,
    Disable,
    Respawn
}

public static class DumbWays // idk why would you ever want to do anything else other than destroy the object why did i make this wtf
{                           // future wolfie: thank fuck you made this, this decopules death from health and is modular and would be hell to setup later
    public static Dictionary<DumbWaysToDieEnum,Action<GameObject>> ToDie;

    static DumbWays()
    {
        ToDie = new Dictionary<DumbWaysToDieEnum,Action<GameObject>>();

        ToDie[DumbWaysToDieEnum.Destroy] = _Destroy;
        ToDie[DumbWaysToDieEnum.Disable] = Disable;
        ToDie[DumbWaysToDieEnum.Respawn] = Respawn;
    }

    static void _Destroy(GameObject gobj)
    {
        UnityEngine.Object.Destroy(gobj);
    }
    static void Disable(GameObject gobj)
    {
        gobj.SetActive(false);
    }
    static void Respawn(GameObject gobj)
    {
        System_Teleporter.Instance.Teleport(TeleportType.Respawn);
        Debug.Log("Player HELLA died");
    }
}
