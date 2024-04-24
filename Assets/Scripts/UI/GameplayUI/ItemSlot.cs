using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Text _text;

    public void UpdateVisual(int amount)
    {
        _text.text = amount.ToString();
    }
}
