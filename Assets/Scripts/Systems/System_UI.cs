using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_UI : MonoBehaviour
{
    public static System_UI Instance;
    private void Awake()
    {
        Instance = this;
    }
}
