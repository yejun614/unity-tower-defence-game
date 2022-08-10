using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScrollView : MonoBehaviour
{
    public float scrollY = 150;
    public float scrollSpeed = 10.0f;
    public RectTransform toggleButton;
    
    private bool _isOpen = false;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_isOpen)
        {
            OpenView();
        }
        else
        {
            CloseView();
        }
    }

    private void OpenView()
    {
        if (_rectTransform.anchoredPosition.y < scrollY)
        {
            Vector2 anchoredPosition = _rectTransform.anchoredPosition;
            anchoredPosition.y += scrollSpeed;
            
            _rectTransform.anchoredPosition = anchoredPosition;
        }
    }
    
    private void CloseView()
    {
        if (_rectTransform.anchoredPosition.y > -scrollY)
        {
            Vector2 anchoredPosition = _rectTransform.anchoredPosition;
            anchoredPosition.y -= scrollSpeed;
            
            _rectTransform.anchoredPosition = anchoredPosition;
        }
    }

    public void ToggleView()
    {
        _isOpen = !_isOpen;

        if (_isOpen)
        {
            toggleButton.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            toggleButton.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
