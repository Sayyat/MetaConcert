using UnityEngine;

namespace Bot
{
    public class Turret : MonoBehaviour
    {
        public Transform yawSegment;
        public Transform pitchSegment;
        public float yawSpeed = 30f;
        public float pitchSpeed = 30f;
        public float yawLimit = 90f;
        public float pitchLimit = 90f;
        public Vector3 target;

        private Quaternion _yawSegmentStartRotation;
        private Quaternion _pitchSegmentStartRotation;

        public void Start()
        {
            _yawSegmentStartRotation = yawSegment.localRotation;
            _pitchSegmentStartRotation = pitchSegment.localRotation;
        }

        public void Update()
        {
            float angle = 0.0f;
            var targetRelative = default(Vector3);
            var targetRotation = default(Quaternion);
            if (this.yawSegment && (this.yawLimit != 0f))
            {
                targetRelative = this.yawSegment.InverseTransformPoint(this.target);
                angle = Mathf.Atan2(targetRelative.x, targetRelative.z) * Mathf.Rad2Deg;
                if (angle >= 180f) angle = 180f - angle;
                if (angle <= -180f) angle = -180f + angle;
                targetRotation = this.yawSegment.rotation * Quaternion.Euler(0f,
                    Mathf.Clamp(angle, -this.yawSpeed * Time.deltaTime, this.yawSpeed * Time.deltaTime), 0f);
                if ((this.yawLimit < 360f) && (this.yawLimit > 0f))
                    this.yawSegment.rotation = Quaternion.RotateTowards(
                        // this.yawSegment.parent.rotation *
                        this._yawSegmentStartRotation, targetRotation, this.yawLimit);
                else this.yawSegment.rotation = targetRotation;
            }

            if (this.pitchSegment && (this.pitchLimit != 0f))
            {
                targetRelative = this.pitchSegment.InverseTransformPoint(this.target);
                angle = -Mathf.Atan2(targetRelative.y, targetRelative.z) * Mathf.Rad2Deg;
                if (angle >= 180f) angle = 180f - angle;
                if (angle <= -180f) angle = -180f + angle;
                targetRotation = this.pitchSegment.rotation * Quaternion.Euler(
                    Mathf.Clamp(angle, -this.pitchSpeed * Time.deltaTime, this.pitchSpeed * Time.deltaTime), 0f, 0f);
                if ((this.pitchLimit < 360f) && (this.pitchLimit > 0f))
                    this.pitchSegment.rotation = Quaternion.RotateTowards(
                        // this.pitchSegment.parent.rotation *
                        this._pitchSegmentStartRotation, targetRotation,
                        this.pitchLimit);
                else this.pitchSegment.rotation = targetRotation;
            }

            Debug.DrawLine(this.pitchSegment.position, this.target, Color.red);
            Debug.DrawRay(this.pitchSegment.position,
                this.pitchSegment.forward * (this.target - this.pitchSegment.position).magnitude, Color.green);
        }

        public void Target(Vector3 target)
        {
            this.target = target;
        }
    }
}