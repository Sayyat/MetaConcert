using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DancePanel : MonoBehaviour
    {
        [SerializeField] private GameObject hello;
        [SerializeField] private GameObject applause;
        [SerializeField] private GameObject dance1;
        [SerializeField] private GameObject dance2;
        [SerializeField] private GameObject dance3;


        private List<GameObject> _myButtons;
        public Vector3 InitialPosition { get; set; }

        private void Awake()
        {
            InitialPosition = transform.parent.position;
            _myButtons = new List<GameObject>()
            {
                hello,
                applause,
                dance1,
                dance2,
                dance3
            };
        }

        private void OnEnable()
        {
            ShowPanels();
        }


        public void ShowPanels()
        {
            foreach (var button in _myButtons)
            {
               button.transform.DOMove(InitialPosition, 2).From();
            }
        }
    }
}