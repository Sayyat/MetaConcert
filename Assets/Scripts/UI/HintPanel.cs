using UnityEngine;
using UnityEngine.UI;

namespace UI
{
   public class HintPanel : MonoBehaviour
   {
      [SerializeField] private Button close;

      private void OnEnable()
      {
         close.onClick.AddListener(() => gameObject.SetActive(false));
      }

   
      private void OnDisable()
      {
         close.onClick.RemoveAllListeners();
      }


      public void Toggle()
      {
         gameObject.SetActive(!gameObject.activeSelf);
      }
   }
}
