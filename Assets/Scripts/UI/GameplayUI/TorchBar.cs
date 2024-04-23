using System.Collections;
using System.Collections.Generic;
using GJam.Player;
using UnityEngine;
using UnityEngine.UI;

public class TorchBar : MonoBehaviour
{
    [SerializeField] private Image _torchImg;

    private void Start()
    {
        PlayerController.Instance.Torch.OnFuelChange += UpdateVisual;
    }

    private void UpdateVisual(float pct)
    {
        _torchImg.fillAmount = pct;
    }

}