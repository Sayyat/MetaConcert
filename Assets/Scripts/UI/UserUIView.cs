using System;
using Assets.Scripts.UI;
using DG.Tweening;
using Goods;
using Photon.Chat.Demo;
using Quest;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UserUIView : MonoBehaviour
    {
        [SerializeField] private UICanvasControllerInput mobileInput;
        [SerializeField] private MobileDisableAutoSwitchControls mobileDisableAutoSwitchControls;
        [SerializeField] private UserButtonsView userButtonsView;
        [SerializeField] private ProductViewPanel productViewPanel;
        [SerializeField] private HintPanel hintPanel;
        [SerializeField] private SettingsPanel settingsPanel;
        [SerializeField] private ScreenshotPanel screenshotPanel;
        [SerializeField] private NamePickGui namePickGui;
        [SerializeField] private CoinProgress coinProgress;

        private Image _background;


        public UICanvasControllerInput MobileInput => mobileInput;
        public MobileDisableAutoSwitchControls MobileDisableAutoSwitchControls => mobileDisableAutoSwitchControls;
        public UserButtonsView UserButtonsView => userButtonsView;
        public ProductViewPanel ProductViewPanel => productViewPanel;
        public HintPanel HintPanel => hintPanel;
        public SettingsPanel SettingsPanel => settingsPanel;
        public ScreenshotPanel ScreenshotPanel => screenshotPanel;
        public NamePickGui NamePickGui => namePickGui;
        public CoinProgress CoinProgress => coinProgress;

        private void Awake()
        {
            _background = GetComponent<Image>();
        }

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
#if UNITY_ANDROID || UNITY_WEBGL
            mobileInput.gameObject.SetActive(true);
#else
            mobileInput.gameObject.SetActive(false);
            userButtonsView.Screenshot.onClick.AddListener(TakeScreenshot);
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


        private void TakeScreenshot()
        {
            // take screenshot
            ScreenshotPanel.gameObject.SetActive(true);  
            
            // blink effect
            var sequence = DOTween.Sequence();
            sequence.Append(_background.DOColor(new Color(1, 1, 1, 1), 0.5f));
            sequence.Append(_background.DOColor(new Color(1, 1, 1, 0), 0f));
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