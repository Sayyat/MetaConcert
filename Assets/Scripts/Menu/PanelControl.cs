using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
    public class PanelControl : MonoBehaviour
    {

        [SerializeField] private Button enterButton;


        private void Start()
        {
            enterButton.onClick.AddListener(() =>
            {
                EventSystem.current.enabled = false;
                Debug.LogError("EventSystemIsOff-HardOff");
            });
        }

        private void OnDestroy()
        {
            enterButton.onClick.RemoveAllListeners();
        }
    }
}
