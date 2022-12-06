using System;
using UnityEngine;

namespace Effects
{
    public class RotateEffect : MonoBehaviour
    {
        [SerializeField] private float speed = 2;
        [SerializeField] private VectorRotate vectorRotate = VectorRotate.Up;
        
        private Vector3 _rotation = Vector3.up;

        private void Start()
        {
            _rotation = vectorRotate switch
            {
                VectorRotate.Up => Vector3.up,
                VectorRotate.Down => Vector3.down,
                VectorRotate.Left => Vector3.left,
                VectorRotate.Right => Vector3.right,
                VectorRotate.Back => Vector3.back,
                VectorRotate.Forward => Vector3.forward,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void Update()
        {
            gameObject.transform.Rotate(_rotation * speed * Time.deltaTime);
        }

        private enum VectorRotate
        {
            Up,
            Down,
            Left,
            Right,
            Back,
            Forward
        }
    }
}
