using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Goods
{
    public class Goods : MonoBehaviour
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
