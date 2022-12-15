using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lift
{
    public class Button3d : MonoBehaviour
    {
        [SerializeField] private int floor;

        public event Action<int> ButtonClicked;
        
        private void OnMouseDown()
        {
            ButtonClicked?.Invoke(floor);
        }
    }
}
