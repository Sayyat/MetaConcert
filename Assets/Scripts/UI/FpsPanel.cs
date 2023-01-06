using TMPro;
using UnityEngine;

namespace UI
{
    public class FpsPanel: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fps;

        private void Update()
        {
            var rate = Mathf.Round(1f / Time.deltaTime);
            fps.text = $"{rate} hz";
        }
    }
}