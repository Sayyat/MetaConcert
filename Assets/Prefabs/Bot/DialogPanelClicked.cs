using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogPanelClicked : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textInDialog;

    private string[] phrases = new[]
    {
        "Hello",
        "Welcome to Metaworld",
        "My name is Android",
        "How can I help you"
    };
}
