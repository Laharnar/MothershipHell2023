using UnityEngine;

namespace Combat.AI
{
    public class MouseAim : ReactiveBase
    {
        public ReactiveBase next;
        public bool mouseAim = true;
        public override bool React(Outputs outputs)
        {
            if (mouseAim)
            {
                var camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                camPos.z = transform.position.z;
                transform.up = camPos-transform.position;
            }
            return lastResult = !next ? true : next.React(outputs);
        }
    }
}