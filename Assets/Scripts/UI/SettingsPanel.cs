using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Button close;
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
            sensibilitySlider.onValueChanged.AddListener((value) => virtualTouchZone.magnitudeMultiplier = value);
            musicSlider.onValueChanged.AddListener((value) => _audioSource.volume = value);
        }


        private void OnDisable()
        {
            close.onClick.RemoveAllListeners();
            sensibilitySlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.RemoveAllListeners();
        }


        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}