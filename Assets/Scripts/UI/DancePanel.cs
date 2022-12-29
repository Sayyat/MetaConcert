using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class DancePanel : MonoBehaviour
    {
        [SerializeField] private Button hello;
        [SerializeField] private Button applause;
        [SerializeField] private Button dance1;
        [SerializeField] private Button dance2;
        [SerializeField] private Button dance3;
        [SerializeField] private RectTransform commonInitialPosition;


        public Button Hello => hello;
        public Button Applause => applause;
        public Button Dance1 => dance1;
        public Button Dance2 => dance2;
        public Button Dance3 => dance3;

        private Vector2 _commonInitialPosition;
        private Vector2 _helloInitialPosition;
        private Vector2 _applauseInitialPosition;
        private Vector2 _dance1InitialPosition;
        private Vector2 _dance2InitialPosition;
        private Vector2 _dance3InitialPosition;


        public bool isVisible = false;
        public bool isAnimating = false;


        private List<Button> _myButtons;
        private List<Vector2> _initialPositions;

        private void Awake()
        {
            isVisible = false;
            _commonInitialPosition = commonInitialPosition.anchoredPosition;
            _helloInitialPosition = hello.GetComponent<RectTransform>().anchoredPosition;
            _applauseInitialPosition = applause.GetComponent<RectTransform>().anchoredPosition;
            _dance1InitialPosition = dance1.GetComponent<RectTransform>().anchoredPosition;
            _dance2InitialPosition = dance2.GetComponent<RectTransform>().anchoredPosition;
            _dance3InitialPosition = dance3.GetComponent<RectTransform>().anchoredPosition;

            _myButtons = new List<Button>()
            {
                hello,
                applause,
                dance1,
                dance2,
                dance3
            };

            _initialPositions = new List<Vector2>()
            {
                _helloInitialPosition,
                _applauseInitialPosition,
                _dance1InitialPosition,
                _dance2InitialPosition,
                _dance3InitialPosition
            };
        }

        private void Start()
        {
            _myButtons.ForEach(button =>
            {
                button.GetComponent<RectTransform>().anchoredPosition = _commonInitialPosition;
            });
        }
        
        

        public void Toggle()
        {
            Debug.Log("DancePanelToggle");
            if (isVisible)
            {
                HidePanel(0.3f);
            }
            else
            {
                ShowPanel(0.3f);
            }
        }

        private void ShowPanel(float duration)
        {
            if (isAnimating) return;

            isAnimating = true;
            var seq = DOTween.Sequence();

            for (var i = 0; i < _myButtons.Count; i++)
            {
                var button = _myButtons[i];
                var loop = button.GetComponent<RectTransform>().DOAnchorPos(_initialPositions[i], duration);
                seq.Join(loop);
            }
            seq.OnComplete(() =>
            {
                Debug.Log("<Color=Blue>OnComplete</Color>");

                isVisible = true;
                isAnimating = false;
                seq.Kill();
            });
        }


        private void HidePanel(float duration)
        {
            if (isAnimating) return;

            isAnimating = true;
            var seq = DOTween.Sequence();

            foreach (var button in _myButtons)
            {
                var loop = button.GetComponent<RectTransform>().DOAnchorPos(_commonInitialPosition, duration);
                seq.Join(loop);
            }

            seq.OnComplete(() =>
            {
                Debug.Log("<Color=Blue>OnComplete</Color>");
                isVisible = false;
                isAnimating = false;
                seq.Kill();
            });
        }


        public void OnDestroy()
        {
            Debug.Log("Ondestroy");
            DOTween.KillAll(true);
        }
    }
}