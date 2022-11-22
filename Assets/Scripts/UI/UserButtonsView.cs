using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UserButtonsView : MonoBehaviour
    {
        [SerializeField] private Button likes;
        [SerializeField] private TMP_Text likesCount;
        [SerializeField] private Button videoSettings;
        [SerializeField] private Button playerSettings;
        [SerializeField] private Button quit;
        [SerializeField] private Button help;
        [SerializeField] private Button more;
        [SerializeField] private Button onMicrophone;
        [SerializeField] private Button onCamera;
        [SerializeField] private Button onChat;

        private List<Button> _buttons = new List<Button>();

        private void Start()
        {
            InitialiseButtons();
            likesCount.text = "ZIZ";
        }

        private void InitialiseButtons()
        {
           _buttons.Add(likes);
           _buttons.Add(videoSettings);
           _buttons.Add(playerSettings);
           _buttons.Add(quit);
           _buttons.Add(help);
           _buttons.Add(more);
           _buttons.Add(onChat);
           _buttons.Add(onCamera);
           _buttons.Add(onMicrophone);

           foreach (var button in _buttons)
           {
              var controller = button.gameObject.AddComponent<BtnViewController>();
              controller.CurrentButton = button;
              controller.percentScale = 10f;
           }
        }
    }
}