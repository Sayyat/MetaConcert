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

        private Vector3 _commonInitialPosition;
        private Vector3 _helloInitialPosition;
        private Vector3 _applauseInitialPosition;
        private Vector3 _dance1InitialPosition;
        private Vector3 _dance2InitialPosition;
        private Vector3 _dance3InitialPosition;


        public bool IsVisible = true;
        public bool IsAnimating = false;


        private List<Button> _myButtons;
        private List<Vector3> _initialPositions;


        public void Init()
        {
            IsVisible = true;
            _commonInitialPosition = commonInitialPosition.position;
            _helloInitialPosition =  hello.GetComponent<RectTransform>().position;
            _applauseInitialPosition =  applause.GetComponent<RectTransform>().position;
            _dance1InitialPosition = dance1.GetComponent<RectTransform>().position;
            _dance2InitialPosition = dance2.GetComponent<RectTransform>().position;
            _dance3InitialPosition = dance3.GetComponent<RectTransform>().position;

            _myButtons = new List<Button>()
            {
                hello,
                applause,
                dance1,
                dance2,
                dance3
            };

            _initialPositions = new List<Vector3>()
            {
                _helloInitialPosition,
                _applauseInitialPosition,
                _dance1InitialPosition,
                _dance2InitialPosition,
                _dance3InitialPosition
            };
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
                HidePanel();
            }
            else
            {
                ShowPanel();
            }
        }

        private void ShowPanel()
        {
            if (IsAnimating) return;

            IsAnimating = true;
            var seq = DOTween.Sequence();

            for (var i = 0; i < _myButtons.Count; i++)
            {
                var button = _myButtons[i];
                var loop = button.GetComponent<RectTransform>().DOMove(_initialPositions[i], 0.5f);
                seq.Join(loop);
            }

            seq.onComplete = () =>
            {
                IsVisible = true;
                IsAnimating = false;
            };
        }


        private void HidePanel()
        {
            if (IsAnimating) return;

            IsAnimating = true;
            var seq = DOTween.Sequence();

            foreach (var button in _myButtons)
            {
                var loop = button.GetComponent<RectTransform>().DOMove(_commonInitialPosition, 0.5f);
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