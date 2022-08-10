using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeathPointSlider : MonoBehaviour
{
    public GameManager gameManager;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        _slider.value = gameManager.heathPoint * 0.01f;
    }
}
