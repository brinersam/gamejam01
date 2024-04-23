using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("clicked!");
        transform.localScale *= 1.5f;
    }

}
