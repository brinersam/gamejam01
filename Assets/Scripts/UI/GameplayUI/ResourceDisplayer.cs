using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplayer : MonoBehaviour
{
    [SerializeField] private ResourceTypeEnum _type;
    [SerializeField] private Text _text;

    public void UpdateVisuals(int amount)
    {
        _text.text = amount.ToString();
    }
}
