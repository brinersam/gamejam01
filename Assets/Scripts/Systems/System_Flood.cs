using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Flood : MonoBehaviour
{
    [SerializeField] private EscapedFloodTrigger _trigger;
    [SerializeField] private GameObject _escapeLadder;

    public bool InProgress = false;
    public static System_Flood Instance;
    public CheckPointMover Lava;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _escapeLadder.SetActive(false);
    }

    public void StartEndSequence()
    {
        _escapeLadder.SetActive(true);
        _trigger.gameObject.SetActive(true);
        InProgress = true;
        PlayerController.Instance.Torch.StopTorchFromRunningOut();
        Lava.StartMoving();
    }

    public void PlayerReachedSurface()
    {
        Debug.Log("gamer on surface");
    }
}
