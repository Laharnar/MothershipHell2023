using UnityEngine;

namespace Combat.AI
{
    public class ReactiveBase:MonoBehaviour
    {
        public bool lastResult;

        // true if chain should continue
        public virtual bool React(Outputs outputs)
        {
            return lastResult = true;
        }
    }
}