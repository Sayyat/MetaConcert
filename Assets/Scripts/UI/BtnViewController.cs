using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI

{
    public class BtnViewController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Range(0, 100)] [SerializeField] public float percentScale = 4;

        private Vector3 _scaleVector = Vector3.one;

        public Button CurrentButton { get; set; }
        public float PercentScale => percentScale;


        public void OnPointerEnter(PointerEventData eventData)
        {
            CurrentButton.transform.localScale = new Vector3(_scaleVector.x + (PercentScale / 100),
                _scaleVector.y + (PercentScale / 100), _scaleVector.z + (PercentScale / 100));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CurrentButton.transform.localScale = _scaleVector;
        }
    }
}