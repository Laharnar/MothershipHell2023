
using UnityEngine;

namespace Combat.AI
{
    public class DestroySimple : ReactiveBase
    {
        public Spawned spawned;
        public override bool React(Outputs outputs)
        {
            if (spawned)
                spawned.Destroy();
            else Destroy(gameObject);
            return true;
        }
    }
}