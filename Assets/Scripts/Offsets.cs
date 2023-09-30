using UnityEngine;

namespace Combat.AI
{
    public class Offsets : ReactiveNext
    {
        public Vector3 degrees;

        public bool startOnly = true;


        private void Start()
        {
            if(startOnly)
            React(null);
        }

        public override bool React(Outputs outputs)
        {
            if(!startOnly)
            transform.Rotate(degrees);
            return base.React(outputs);
        }
    }
}