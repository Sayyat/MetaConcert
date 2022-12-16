using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
   public class HintPanel : MonoBehaviour
   {
      private Button _close;

      private void Awake()
      {
         _close = GetComponent<Button>();
      }

      private void OnEnable()
      {
         _close.onClick.AddListener(() => gameObject.SetActive(false));
      }

   
      private void OnDisable()
      {
         _close.onClick.RemoveAllListeners();
      }


      public void Toggle()
      {
         gameObject.SetActive(!gameObject.activeSelf);
      }
   }
}
