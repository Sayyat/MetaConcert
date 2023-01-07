using System;
using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScreenshotPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI info;
        [SerializeField] private Button openFolder;
        private const string Pattern = @"yyyyMMddHHmmssfff";


        private void OnEnable()
        {
            StartCoroutine(TakeScreenshot());
            openFolder.onClick.AddListener(() => Process.Start(Application.persistentDataPath));
        }

        public IEnumerator TakeScreenshot()
        {
            var date = DateTime.Now.ToString(Pattern);
            var path = Application.persistentDataPath + $"/{date}.png";
            ScreenCapture.CaptureScreenshot(path);
            info.text = $"Скриншот сохранен: {date}.png";
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            openFolder.onClick.RemoveAllListeners();
        }
    }
}