using System;
using Assets.Scripts.UI;
using Goods;
using StarterAssets;
using UnityEngine;

namespace UI
{
    public class UserUIView : MonoBehaviour
    {
        [SerializeField] private UICanvasControllerInput mobileInput;
        [SerializeField] private UserButtonsView userButtonsView;
        [SerializeField] private ProductViewPanel productViewPanel;
        [SerializeField] private HintPanel hintPanel;
        [SerializeField] private SettingsPanel settingsPanel;

        public UICanvasControllerInput MobileInput => mobileInput;
        public UserButtonsView UserButtonsView => userButtonsView;
        public ProductViewPanel ProductViewPanel => productViewPanel;
        public HintPanel HintPanel => hintPanel;
        public SettingsPanel SettingsPanel => settingsPanel;

        private void Start()
        {
            userButtonsView.MobileUIToggle.gameObject.SetActive(false);
            userButtonsView.Help.onClick.AddListener(() => hintPanel.Toggle());
            userButtonsView.PlayerSettings.onClick.AddListener(() => settingsPanel.Toggle());


#if !UNITY_ANDROID
            userButtonsView.MobileUIToggle.gameObject.SetActive(false);

#else
            userButtonsView.MobileUIToggle.gameObject.SetActive(true);
            userButtonsView.MobileUIToggle.onClick.AddListener(() =>
            {
                mobileInput.gameObject.SetActive(!mobileInput.gameObject.activeSelf);
            });
#endif
        }


        private void OnDestroy()
        {
            userButtonsView.Help.onClick.RemoveAllListeners();
            userButtonsView.PlayerSettings.onClick.RemoveAllListeners();
        }
    }
}