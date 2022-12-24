using System;
using Assets.Scripts.UI;
using Goods;
using Photon.Chat.Demo;
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
        [SerializeField] private ScreenshotPanel screenshotPanel;
        [SerializeField] private NamePickGui namePickGui;

        public UICanvasControllerInput MobileInput => mobileInput;
        public UserButtonsView UserButtonsView => userButtonsView;
        public ProductViewPanel ProductViewPanel => productViewPanel;
        public HintPanel HintPanel => hintPanel;
        public SettingsPanel SettingsPanel => settingsPanel;
        public ScreenshotPanel ScreenshotPanel => screenshotPanel;
        public NamePickGui NamePickGui => namePickGui;

        private void Start()
        {
            userButtonsView.MobileUIToggle.gameObject.SetActive(false);
            userButtonsView.Help.onClick.AddListener(() => hintPanel.Toggle());
            userButtonsView.PlayerSettings.onClick.AddListener(() => settingsPanel.Toggle());
            userButtonsView.Chat.onClick.AddListener(() =>
            {
                var parent = NamePickGui.gameObject.transform.parent.gameObject;
                parent.SetActive(!parent.activeSelf);
            });
#if UNITY_ANDROID
            mobileInput.gameObject.SetActive(true);
#else
            mobileInput.gameObject.SetActive(false);
            userButtonsView.Screenshot.onClick.AddListener(() => ScreenshotPanel.gameObject.SetActive(true));
#endif

#if !UNITY_WEBGL
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
#if !UNITY_ANDROID
            userButtonsView.Screenshot.onClick.RemoveAllListeners();
#endif
        }
    }
}