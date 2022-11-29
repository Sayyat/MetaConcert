using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lift
{
    public class Button3d : MonoBehaviour, IButton3d
    {
        [SerializeField] private string where;
        [SerializeField] private int floor;

        public event Action<string, int> ButtonClicked;
        
        private void OnMouseDown()
        {
            Debug.Log($"<Color=Green>Button clicked: {where}: {floor}</Color>");
            ButtonClicked?.Invoke(where, floor);
        }
    }
}
