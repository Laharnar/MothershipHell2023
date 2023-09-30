
using UnityEngine;

namespace Combat.AI
{
    public class DestroySimple : ReactiveBase
    {
        [Header("Destroys properly(pooled)")]
        public Spawned spawned;
        public bool autodestroy = false;

        public override bool React(Outputs outputs)
        {
            if (spawned)
                spawned.Destroy();
            else Destroy(gameObject);
            return true;
        }

        private void OnDestroy()
        {
            if (autodestroy)
            {
                React(null);
            }
        }
    }
}