using UnityEngine;

namespace Combat.AI
{
    public class BounceApproach : ReactiveNext
    {
        public Transform target;
        public Transform bounceBack;
        public float moveSpeed = 1;
        public float backMoveSpeed = 1;
        public ReactiveBase loopEndHold;
        int mode = 0;
        float mode1Switch = 0;
        public float inverse;
        public float inverseback;
        public float timeout = 5;
        float timeLeft;

        public override bool React(Outputs outputs)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                if (mode == 1) mode = 0;
                else mode = 1;
                timeLeft = timeout;
            }
            if (mode == 0 && target)
            {
                var dir = target.position - transform.position;
                if (Vector2.Distance(transform.position, target.position) > 0.1f)
                {
                    float attr = (dir.magnitude * moveSpeed + inverse / dir.magnitude);
                    transform.position += dir.normalized * attr * Time.deltaTime;
                }
                else
                {
                    mode = 1;
                    timeLeft = timeout;
                }
            }
            else if (mode == 1 && bounceBack) {
                mode1Switch = Time.time;
                var dir = bounceBack.position - transform.position;
                if (Vector2.Distance(transform.position, bounceBack.position) > 0.1f)
                {
                    var attr = (dir.magnitude / backMoveSpeed + inverseback / dir.magnitude);
                    transform.position += dir.normalized * attr * Time.deltaTime;
                }
                else {
                    if (loopEndHold == null || loopEndHold.React(outputs))
                    {
                        timeLeft = timeout;
                        mode = 0;
                    }
                }
            }

            return base.React(outputs);
        }
    }
}