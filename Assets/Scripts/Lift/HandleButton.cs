using UnityEngine;

namespace Lift
{
    public class HandleButton : MonoBehaviour
    {
        [SerializeField] private Button3d button;


        private void Awake()
        {
            button.ButtonClicked += (s, i) =>
            {
                Debug.Log($"Output {s} : {i}");
            };
        }
    }
}
