using DG.Tweening;
using UnityEngine;

namespace Effects
{
    public class OffsetMaterial : MonoBehaviour
    {
        private Renderer _rend;
        private Material _material;
        private Vector2 _offsetBaseMap;
        public float _speedX = -0.6f;
        public float _speedY = -0.1f;

        private void Start()
        {
            _rend = GetComponent<Renderer>();
            _material = _rend.material;
            _material.DOOffset(new Vector2(2f, 0f), "_BaseMap", 5).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            
        }
    }
}