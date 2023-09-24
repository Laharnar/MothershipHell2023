using System.Collections.Generic;
using UnityEngine;

namespace Combat.AI
{
    public class Raycast2D : ReactiveBase
    {
        public ReactiveBase next;
        public bool clean = false;
        public Transform hasRaycast;
        public override bool React(Outputs outputs)
        {
            var pyh = Physics2D.Raycast(transform.position, Vector2.up);
            if (clean)
                outputs.At<List<GameObject>>(A.detection).Clear();
            if (pyh.transform != null)
                outputs.At<List<GameObject>>(A.detection).Add(pyh.transform.gameObject);
            hasRaycast = pyh.transform;
            return lastResult = next ? next.React(outputs) : true;
        }
    }
}