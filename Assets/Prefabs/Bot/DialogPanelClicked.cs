
using System;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanelClicked : MonoBehaviour
{
    private const byte StartedDialogue = 0;

    [SerializeField] private TextMeshProUGUI textInDialog;
    [SerializeField] private Button _dialogPanel;

    private int _numDialog;

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
        transform.localScale = Vector3.zero;

        DG.Tweening.Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(1, 1));
        s.AppendInterval(2f);
        s.Append(transform.DOScale(0.01f, 0.01f).OnComplete(NextPhrase));
        s.SetLoops(-1, LoopType.Restart);
    }

    private void NextPhrase()
    {
        if (_phrases == null)
        {
            textInDialog.text = "Bye-Bye!";
            return;
        }
        
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

    private void OnDisable()
    {
        _numDialog = StartedDialogue;
        textInDialog.text = _phrases[_numDialog];
        transform.localScale = Vector3.zero;
    }
}