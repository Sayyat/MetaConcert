using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBotController : MonoBehaviour
{
    [SerializeField] private GameObject _dialog;
    [SerializeField] private DialogPanelClicked botDialog;

    private void Start()
    {
        _dialog.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
           _dialog.SetActive(true);
           botDialog.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _dialog.SetActive(false);
            botDialog.enabled = true;
        }
    }
}
