using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Button close;
        [SerializeField] private Button openScreenshotsFolder;
        [SerializeField] private Button soundOn;
        [SerializeField] private Button soundOff;
        [SerializeField] private Button sensibilityButton;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sensibilitySlider;

        [SerializeField] private UIVirtualTouchZone virtualTouchZone;

        private AudioSource _audioSource;


        private float _lastSoundVolume;

        private void Awake()
        {
            _audioSource = GameObject.Find("BackgroundMusic(Clone)").GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            soundOn.gameObject.SetActive(false);
            close.onClick.AddListener(() => gameObject.SetActive(false));
            openScreenshotsFolder.onClick.AddListener(() => Process.Start(Application.persistentDataPath));
            musicSlider.onValueChanged.AddListener((value) => _audioSource.volume = value);
            sensibilitySlider.onValueChanged.AddListener((value) => virtualTouchZone.magnitudeMultiplier = value);
            soundOn.onClick.AddListener(() =>
            {
                soundOn.gameObject.SetActive(false);
                soundOff.gameObject.SetActive(true);
                musicSlider.interactable = true;
                musicSlider.fillRect.GetComponent<Image>().color = Color.black;
                // musicSlider.GetComponent<Image>().color = Color.black;   

                _audioSource.volume = _lastSoundVolume;
            });
            soundOff.onClick.AddListener(() =>
            {
                soundOn.gameObject.SetActive(true);
                soundOff.gameObject.SetActive(false);
                musicSlider.interactable = false;
                musicSlider.fillRect.GetComponent<Image>().color = Color.gray;
                // musicSlider.GetComponent<Image>().color = Color.gray;    

                _lastSoundVolume = _audioSource.volume;
                _audioSource.volume = 0f;
            });
            sensibilityButton.onClick.AddListener(() => sensibilitySlider.value = 10f);
        }


        private void OnDisable()
        {
            close.onClick.RemoveAllListeners();
            openScreenshotsFolder.onClick.RemoveAllListeners();
            musicSlider.onValueChanged.RemoveAllListeners();
            sensibilitySlider.onValueChanged.RemoveAllListeners();
            soundOn.onClick.RemoveAllListeners();
            soundOff.onClick.RemoveAllListeners();
            sensibilityButton.onClick.RemoveAllListeners();
        }


        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}