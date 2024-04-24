using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Flood : MonoBehaviour
{
    public static System_Flood Instance;
    public CheckPointMover Lava;
    private void Awake()
    {
        Instance = this;
    }

    public void StartEndSequence()
    {
        Lava.StartMoving();
    }
}
