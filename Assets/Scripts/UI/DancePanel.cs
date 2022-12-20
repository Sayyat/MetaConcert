using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
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


        public bool IsVisible = true;
        public bool IsAnimating = false;


        private List<Button> _myButtons;
        private List<Vector2> _initialPositions;


        public void Init()
        {
            IsVisible = true;
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

            // hide dance panel when start
            HidePanel(0f);
        }

        // private void OnEnable()
        // {
        //     ShowPanel();
        // }
        //
        // private void OnDisable()
        // {
        //     IsVisible = false;
        // }

        public void Toggle()
        {
            if (IsVisible)
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
            if (IsAnimating) return;

            IsAnimating = true;
            var seq = DOTween.Sequence();

            for (var i = 0; i < _myButtons.Count; i++)
            {
                var button = _myButtons[i];
                var loop = button.GetComponent<RectTransform>().DOAnchorPos(_initialPositions[i], duration);
                seq.Join(loop);
            }

            seq.OnComplete(() =>
            {
                IsVisible = true;
                IsAnimating = false;
            });
        }


        private void HidePanel(float duration)
        {
            if (IsAnimating) return;

            IsAnimating = true;
            var seq = DOTween.Sequence();

            foreach (var button in _myButtons)
            {
                var loop = button.GetComponent<RectTransform>().DOAnchorPos(_commonInitialPosition, duration);
                seq.Join(loop);
            }

            seq.onComplete = () =>
            {
                IsVisible = false;
                IsAnimating = false;
            };
        }
    }
}