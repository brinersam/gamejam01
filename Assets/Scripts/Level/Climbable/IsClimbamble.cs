using System.Collections;
using System.Collections.Generic;
using GJam.Player;
using UnityEngine;

public class IsClimbamble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            player.SetClimbState(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            player.SetClimbState(false);
        }
    }
}
