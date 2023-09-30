using UnityEngine;

namespace Combat.AI
{

    public class Rotation : ReactiveBase
    {
        public bool run = true;
        public float rotationAmount = 1f;
        public ReactiveUnit unit;
        public Register passby;
        public Transform obj;
        public ReactiveBase next;
        public Group targeting;
        public Transform lastTarget;
        public bool mouseAim = false;
        public string castTarget = "nearest";
        public Vector3 lastEulers;

        private void OnValidate()
        {
            if (passby == null) passby = GetComponentInParent<Register>();
        }

        public override ReactiveBase Next(int i = 0)
        {
            return next;
        }

        public override bool React(Outputs outputs)
        {
            if(!run)return lastResult = !next ? true : next.React(outputs);
            if (targeting == null && unit)
                targeting = unit.Group?.enemies[0];

            if (targeting == null && passby)
                targeting = passby.group.enemies[0];
            if (castTarget != "")
                outputs[A.castTarget] = castTarget;
            if (!mouseAim)
            {
                NonMouse(outputs);
            }
            return lastResult = !next ? true : next.React(outputs);
        }

        private void NonMouse(Outputs outputs)
        {
            var targetSelf = transform;
            if (unit != null)
                targetSelf = unit.transform;
            if (obj != null)
                targetSelf = obj.transform;
            if (targeting && targeting.targets.Count > 0)
            {
                lastTarget = targeting.targets[0].transform;

                var aimCode = outputs.At<string>(A.castTarget);
                if (R.nearest == aimCode)
                    lastTarget = targeting.Nearest(targetSelf.position);
                if (R.random == aimCode)
                    lastTarget = targeting.Random();

                //transform.up= lastTarget.transform.position - transform.position;
                var target = lastTarget.position;
                var dir = Vector3.Dot(targetSelf.right, targetSelf.position - target);
                if (dir == 0)
                    dir = Vector3.Dot(targetSelf.right + targetSelf.up, targetSelf.position - target);
                lastEulers = Vector3.forward * dir * Time.deltaTime;
                targetSelf.Rotate(lastEulers);
            }
            else
            {
                targetSelf.Rotate(Vector3.forward * rotationAmount * Time.deltaTime);
            }
        }
    }
}