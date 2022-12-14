using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanelClicked : MonoBehaviour
{
    private const byte StartedDialogue = 0;

    [SerializeField] private TextMeshProUGUI textInDialog;
    [SerializeField] private Button _dialogPanel;

    private byte _numDialog;

    private readonly string[] _phrases = new[]
    {
        "Hello! ...",
        "Welcome to MetaWorld! ...",
        "My name is Android. ...",
        "How can I help you? ..."
    };

    private void Start()
    {
        _numDialog = StartedDialogue;
        textInDialog.text = _phrases[_numDialog];
        _dialogPanel.onClick.AddListener(ClickDialog);
    }

    private void ClickDialog()
    {
        if (_numDialog < _phrases.Length - 1)
        {
            _numDialog++;
            textInDialog.text = _phrases[_numDialog];
        }
        else
        {
            _numDialog = 0;
            textInDialog.text = _phrases[_numDialog];
        }
    }
}