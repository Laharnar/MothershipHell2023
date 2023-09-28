using UnityEngine;

namespace Combat.AI
{

    public class Rotation : ReactiveBase
    {
        public bool run = true;
        public float rotationAmount = 1f;
        public ReactiveUnit unit;
        public ReactiveBase next;
        public Group targeting;
        public Transform lastTarget;
        public bool mouseAim = false;

        public override bool React(Outputs outputs)
        {
            if(!run)return lastResult = !next ? true : next.React(outputs);
            if (targeting == null)
                targeting = unit.Group?.enemies[0];

            if (!mouseAim)
            {
                NonMouse(outputs);
            }
            return lastResult = !next ? true : next.React(outputs);
        }

        private void NonMouse(Outputs outputs)
        {
            if (targeting && targeting.targets.Count > 0)
            {
                lastTarget = targeting.targets[0].transform;

                var aimCode = outputs.At<string>(A.castTarget);
                if (R.nearest == aimCode)
                    lastTarget = targeting.Nearest(transform.position);
                if (R.random == aimCode)
                    lastTarget = targeting.Random();

                //transform.up= lastTarget.transform.position - transform.position;
                var target = lastTarget.position;
                var dir = Vector3.Dot(transform.right, transform.position - target);
                if (dir == 0)
                    dir = Vector3.Dot(transform.right + transform.up, transform.position - target);
                transform.Rotate(Vector3.forward * dir * Time.deltaTime);
            }
            else
            {
                unit.transform.Rotate(Vector3.forward * rotationAmount * Time.deltaTime);
            }
        }
    }
}