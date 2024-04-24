using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapedFloodTrigger : MonoBehaviour
{
    private bool _fired = false;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_fired)
        {
            System_Flood.Instance.PlayerReachedSurface();
        }
    }
}
