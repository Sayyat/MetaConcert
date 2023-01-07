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
            var seq = DOTween.Sequence();
            seq.Append(_material.DOOffset(new Vector2(1f, 0f), "_BaseMap", 2).SetEase(Ease.InCirc));
            seq.Append(_material.DOOffset(new Vector2(0f, 1f), "_BaseMap", 3).SetEase(Ease.InSine));
            seq.Append(_material.DOOffset(new Vector2(-1f, 0f), "_BaseMap", 2).SetEase(Ease.InBounce));
            seq.Append(_material.DOOffset(new Vector2(0f, -1f), "_BaseMap", 3).SetEase(Ease.InElastic));
            seq.SetLoops(-1);
        }
    }
}