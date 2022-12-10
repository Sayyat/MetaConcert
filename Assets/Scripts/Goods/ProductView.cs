using System;
using UnityEngine;

namespace Goods
{
    public class ProductView : MonoBehaviour
    {
        [SerializeField] private int id = 0;

        public event Action<int> ShareMyId;
        private void OnMouseDown()
        {
            Debug.Log("clockOnSite");
            ShareMyId?.Invoke(id);
        }
    }
}
