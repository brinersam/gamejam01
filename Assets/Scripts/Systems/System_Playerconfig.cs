using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Playerconfig : MonoBehaviour
{
    [SerializeField] private int _startingTorchRefills = 2;
    [SerializeField] private int _startingHpRefills = 2;

    public static System_Playerconfig Instance;
    private void Awake()
    {
        Instance = this;
    }

    public int Restore_Torch()
    {
        return _startingTorchRefills;
    }
    public int Restore_HP()
    {
        return _startingHpRefills;
    }
}
