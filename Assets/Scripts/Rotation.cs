using UnityEngine;

namespace Combat.AI
{
    public class Rotation : ReactiveBase
    {
        public float rotationAmount = 1f;
        public ReactiveUnit unit;
        public ReactiveBase next;
        public Group targeting;
        public Transform lastTarget;
        private object aimCode;

        public override bool React(Outputs outputs)
        {
            if (targeting == null)
                targeting = unit.Group?.enemies[0];

            if (targeting && targeting.targets.Count > 0)
            {
                lastTarget = targeting.targets[0].transform;

                var aimCode = outputs.At<string>(A.castTarget);
                if (R.nearest == aimCode)
                    lastTarget = targeting.Nearest(transform.position);
                if(R.random == aimCode)
                    lastTarget = targeting.Random();

                //transform.up= lastTarget.transform.position - transform.position;
                var target = lastTarget.position;
                var dir = Vector3.Dot(transform.right, transform.position - target);
                if (dir == 0)
                    dir = Vector3.Dot(transform.right + transform.up, transform.position - target);
                transform.Rotate(Vector3.forward * dir);
            }
            else
            {
                unit.transform.Rotate(Vector3.forward * rotationAmount * Time.deltaTime);
            }
            return lastResult = !next ? true : next.React(outputs);
        }
    }
}