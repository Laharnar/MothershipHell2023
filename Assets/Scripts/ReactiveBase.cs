using UnityEngine;

namespace Combat.AI
{
    public class ReactiveNext : ReactiveBase
    {
        public ReactiveBase next;
        public override ReactiveBase Next(int i = 0) { return next; }

        // true if chain should continue
        public override bool React(Outputs outputs)
        {
            return lastResult = next ? next.React(outputs) :true;
        }

    }

    public class ReactiveBase:MonoBehaviour
    {
        public bool lastResult;
        public virtual int Count => 1;
        public virtual ReactiveBase Next(int i = 0) { return null; }
        public virtual string Msg => "";

        // true if chain should continue
        public virtual bool React(Outputs outputs)
        {
            return lastResult = true;
        }

        public virtual void OverrideParams(Outputs transfer) { }
    }
}