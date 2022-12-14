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
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sensibilitySlider;

        [SerializeField] private UIVirtualTouchZone virtualTouchZone;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GameObject.Find("BackgroundMusic(Clone)").GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            close.onClick.AddListener(() => gameObject.SetActive(false));
            openScreenshotsFolder.onClick.AddListener(() => OpenFolder(Application.persistentDataPath));
            sensibilitySlider.onValueChanged.AddListener((value) => virtualTouchZone.magnitudeMultiplier = value);
            musicSlider.onValueChanged.AddListener((value) => _audioSource.volume = value);
        }


        private void OnDisable()
        {
            close.onClick.RemoveAllListeners();
            openScreenshotsFolder.onClick.RemoveAllListeners();
            sensibilitySlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.RemoveAllListeners();
        }


        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }


        private void OpenFolder(string path)
        {
            Process.Start(path);
        }
    }
}