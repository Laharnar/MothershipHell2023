using UnityEngine;

namespace Combat.AI
{
    public class RotationTarget : ReactiveNext
    {
        public float lerpRot;
        public Group selfgroup;
        public Transform self;
        public Transform lastTarget;
        public string castTarget = "nearest";

        private void OnValidate()
        {
            if(selfgroup == null) selfgroup = GetComponentInParent<Group>();
            if (self == null) self = transform;
        }

        public override bool React(Outputs outputs)
        {
            var targetSelf = self;
            if (castTarget == R.nearest)
                lastTarget = selfgroup.FirstEnemy()?.Nearest(targetSelf.position);
            var target = lastTarget.position;
            targetSelf.rotation = Quaternion.Lerp(targetSelf.rotation, Quaternion.LookRotation(Vector3.forward, target - targetSelf.position), lerpRot);

            return base.React(outputs);
        }
    }
}