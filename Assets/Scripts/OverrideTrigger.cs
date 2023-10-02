using UnityEngine;

namespace Combat.AI
{
    public class OverrideTrigger : ReactiveNext
    {
        public bool randomizeDir = true;
        public Vector2 inDir;
        public ReactiveBase toOverride;
        public override bool React(Outputs outputs)
        {
            Debug.Log("trigger");
            if (randomizeDir)
                outputs[A.moveDirection] = Random.insideUnitCircle;
            else outputs[A.moveDirection] = inDir;
            toOverride.OverrideParams(outputs);
            return base.React(outputs);
        }
    } 
}