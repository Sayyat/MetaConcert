using UnityEngine;

namespace Bot
{
    public class DialogBotController : MonoBehaviour
    {
        [SerializeField] private GameObject _dialog;
        [SerializeField] private DialogPanelClicked botDialog;
        [SerializeField] private Turret _turret;
        private Vector3 _defaultTarget = Vector3.zero;
        private bool _findTarget;
        private Transform _player;

        private void Start()
        {
            _dialog.SetActive(false);
        }

        private void FixedUpdate()
        {
            if (_findTarget)
            {
                _turret.Target(_player.position);  
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _player = other.transform;
                _findTarget = true;
            
                _dialog.SetActive(true);
                botDialog.enabled = true;
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _findTarget = false;
                _turret.Target(_defaultTarget);
            
                _dialog.SetActive(false);
                botDialog.enabled = true;
            }
        }
    }
}
