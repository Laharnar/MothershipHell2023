using UnityEngine;

namespace Combat.AI
{
    public class Reparent : ReactiveBase
    {
        public bool onStart = true;
        public Transform target;
        public Register selfRegister;

        private void Start()
        {
            if (onStart)
                React(null);
        }

        public override bool React(Outputs outputs)
        {
            transform.parent = target;
            return base.React(outputs);
        }
    }
}