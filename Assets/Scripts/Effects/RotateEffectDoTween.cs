using DG.Tweening;
using UnityEngine;

namespace Effects
{
    public class RotateEffectDoTween : MonoBehaviour
    {
        [SerializeField] private Vector3 speed;
        [SerializeField] private float duration;
        private void Start()
        {
            transform.DORotate(speed, duration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.InBounce);
        }
    }
}
