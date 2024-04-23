using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_UI : MonoBehaviour
{
    [SerializeField] private UIShop _shopUI;

    public static System_UI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void EnableShop(bool yes, bool toggle = false)
    {
        if (toggle)
        {
            _shopUI.gameObject.SetActive(!_shopUI.gameObject.activeSelf);
            return;
        }
            
        _shopUI.gameObject.SetActive(yes);
    }
}
