using UnityEngine;

namespace Combat.AI
{
    public class SpawnPoint : ReactiveBase
    {
        public ReactiveBase next;
        public override ReactiveBase Next(int i = 0)
        {
            return next;
        }

        public override bool React(Outputs outputs)
        {
            base.React(outputs);
            outputs[A.spawnPoint] = transform;

            return lastResult = !next || next.React(outputs);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position + transform.up, new Vector3(0.1f, 0.1f, 0.1f));
        }
    }
}