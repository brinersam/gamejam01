using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaOnHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("player died");
        System_Teleporter.Instance.Teleport(TeleportType.Respawn);
    }

}
