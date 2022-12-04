using System;
using Assets.Scripts.UI;
using StarterAssets;
using UnityEngine;

namespace UI
{
    public class UserUIView : MonoBehaviour
    {
        [SerializeField] private UICanvasControllerInput mobileInput;
        [SerializeField] private UserButtonsView userButtonsView;

        public UICanvasControllerInput MobileInput => mobileInput;
        public UserButtonsView UserButtonsView => userButtonsView;

        private void Start()
        {
            userButtonsView.MobileUIToggle.onClick.AddListener(() =>
            {
                mobileInput.gameObject.SetActive(!mobileInput.gameObject.activeSelf);
            });
        }
    }
}