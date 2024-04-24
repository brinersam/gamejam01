using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsClimbamble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.TryGetComponent(out PlayerController player)) //todo figure out collision 
        // //layers so this will only trigger on player triggers which removes the need for this call
        // {
            PlayerController.Instance.SetClimbState(true);
        // }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if (collision.gameObject.TryGetComponent(out PlayerController player)) //todo figure out collision 
        // //layers so this will only trigger on player triggers which removes the need for this call
        // {
            PlayerController.Instance.SetClimbState(false);
        // }
    }
}
