using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Playerconfig : MonoBehaviour
{
    [SerializeField] private int _startingTorchRefills = 2;
    [SerializeField] private int _startingHpRefills = 2;

    [Range (0,1)] [SerializeField] private float _startingHp = 0.5f;
    [Range (0,1)] [SerializeField] private float _startingTorch = 0.5f;

    public float Start_HP => _startingHp;
    public float Start_Torch => _startingTorch;

    public static System_Playerconfig Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void TorchUpgrade()
    {
        _startingTorchRefills += 1;
    }
    public void HpConsumableUpgrade()
    {
        _startingHpRefills += 1;
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

