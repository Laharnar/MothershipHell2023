using UnityEngine;

namespace Combat.AI
{
    public class ReactiveBase:MonoBehaviour
    {
        public bool lastResult;
        public virtual bool React(Outputs outputs)
        {
            return true;
        }
    }
}