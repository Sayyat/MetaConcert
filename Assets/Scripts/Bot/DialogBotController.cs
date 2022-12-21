using System;
using UnityEngine;

namespace Bot
{
    public class DialogBotController : MonoBehaviour
    {
        [SerializeField] private BotCanvas botCanvas;
        
        private Turret _turret;
        private readonly Vector3 _defaultTarget = Vector3.zero;
        private bool _findTarget;
        private Transform _player;

        private void Awake()
        {
            _turret = GetComponent<Turret>();
        }

        private void Start()
        {
            botCanvas.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_findTarget)
            {
                _turret.Target(_player.position);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            _player = other.transform;
            _findTarget = true;

            botCanvas.gameObject.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            _findTarget = false;
            _turret.Target(_defaultTarget);

            botCanvas.gameObject.SetActive(false);
        }
    }
}