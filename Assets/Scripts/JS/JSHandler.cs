using System;
using UnityEngine;

namespace JS
{
    public class JsHandler : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            SetFullscreen(true);
        }

        public void SetFullscreen(bool state)
        {
            Screen.fullScreen = state;
            Debug.Log("Set fullscreen called");
        }
    }
}
