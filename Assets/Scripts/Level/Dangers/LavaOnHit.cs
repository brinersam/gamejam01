using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaOnHit : MonoBehaviour
{
    [SerializeField] GameObject _UILose;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.Instance.Hide(true);
        PlayerController.Instance.Movement.MovementDisabled = true;
        _UILose.SetActive(true);
    }

}
