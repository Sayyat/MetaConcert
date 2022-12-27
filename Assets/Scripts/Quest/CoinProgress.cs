using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quest
{
    public class CoinProgress : MonoBehaviour
    {
        private int _progress;
        [SerializeField] private List<Image> progressImages;
        [SerializeField] private TextMeshProUGUI progressText;

        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                ShowProgress();
            }
        }

        private void ShowProgress()
        {
            progressText.text = $"{_progress} / 10";

            for (int i = 0; i < _progress; i++)
            {
                progressImages[i].color = new Color(1, 1, 1, 1);
            }
            for (int i = 0; i < _progress; i++)
            {
                progressImages[i].color = new Color(1, 1, 1, 1);
            }
            
            
        }
    }
}