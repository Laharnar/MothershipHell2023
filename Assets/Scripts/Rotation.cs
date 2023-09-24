using UnityEngine;

namespace Combat.AI
{
    public class Rotation : ReactiveBase
    {
        public float rotationAmount = 1f;
        public ReactiveUnit unit;
        public ReactiveBase next;
        public Group targeting;

        public override bool React(Outputs outputs)
        {
            if (targeting && targeting.targets.Count > 0)
            {
                var target = targeting.targets[0].transform.position;
                var dir = Vector3.Dot(transform.right, transform.position - target);
                if (dir == 0)
                    dir = Vector3.Dot(transform.right+transform.up, transform.position - target);
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