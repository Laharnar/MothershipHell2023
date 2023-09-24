using UnityEngine;

namespace Combat.AI
{

    public class Timer : ReactiveBase
    {
        public float rate = 1f;
        public bool startReady = true;
        float time;
        public ReactiveBase next;

        private void Start()
        {
            if (!startReady)
                time = Time.time + rate;
        }

        public override bool React(Outputs outputs)
        {
            if (Time.time > time)
            {
                time = Time.time + rate;
                return lastResult = next != null ? next.React(outputs) : true;
            }
            return lastResult = false;
        }
    }
}