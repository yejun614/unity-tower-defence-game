using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICoinText : MonoBehaviour
{
    public GameManager gameManager;
    
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _text.text = $"{gameManager.coin}";
    }
}
