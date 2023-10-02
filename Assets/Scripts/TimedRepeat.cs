using UnityEngine;

namespace Combat.AI
{
    public class TimedRepeat : ReactiveNext
    {
        public bool started = false;
        float quitTime = 0;
        public float duration = 3;
        public ReactiveBase onTriggered;

        public override ReactiveBase Next(int i = 0)
        {
            if (i == 1)
                return onTriggered;
            return base.Next(i);
        }

        public override int Count => 2;

        public override bool React(Outputs outputs)
        {
            if (started == false)
            {
                started = true;
                quitTime = duration;
                return onTriggered.React(outputs);
            }
            
            return base.React(outputs);
        }

        private void Update()
        {
            if (started)
            {
                quitTime -= Time.deltaTime;
                if (quitTime <= 0)
                    started = false;
            }
        }
    }
}