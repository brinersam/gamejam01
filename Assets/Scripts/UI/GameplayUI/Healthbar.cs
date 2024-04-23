using System.Collections;
using System.Collections.Generic;
using GJam.Player;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image _hpFill;

    private void Start()
    {
        PlayerController.Instance.Health.OnHPChange += UpdateVisual;
    }

    private void UpdateVisual(float pct, bool gated)
    {
        _hpFill.fillAmount = pct;

        if (gated)
            _hpFill.color = Color.cyan;
        else
            _hpFill.color = Color.white;
    }

}
