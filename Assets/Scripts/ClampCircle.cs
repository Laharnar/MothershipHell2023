using UnityEngine;

namespace Combat.AI
{
    public class ClampCircle : ReactiveNext
    {
        public float maxRange = 5;
        public float minRange = 0;
        public Transform target;
        public float lerpMax = 0.5f;
        public float lerpMin = 0.5f;

        public override bool React(Outputs outputs)
        {
            if (target) {
                var dir = transform.position - target.position;
                var dist = dir.magnitude;
                if (dist > maxRange)
                {
                    var goTo = target.position + dir / dist * maxRange;
                    transform.position = Vector3.Lerp(transform.position, goTo, lerpMax);
                    
                }
                if(dist < minRange)
                {
                    var goTo = target.position + dir / dist * minRange;
                    transform.position = Vector3.Lerp(transform.position, goTo, lerpMin);
                }
            }
            return base.React(outputs);
        }

        private void OnDrawGizmosSelected()
        {
            if (target != null) Gizmos.DrawWireSphere(target.position, maxRange);
            Gizmos.color = Color.yellow;
            if (target != null) Gizmos.DrawWireSphere(target.position, minRange);
        }
    }
}