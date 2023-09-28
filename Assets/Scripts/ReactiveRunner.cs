using UnityEngine;

namespace Combat.AI
{
    public class ReactiveRunner : ReactiveNext
    {
        public bool autoRun = true;

        private void OnValidate()
        {
            if (next == null)
                next = GetComponent<ReactiveBase>();
        }

        private void Update()
        {
            if (autoRun)
                React(new Outputs());
        }
    }
    
}