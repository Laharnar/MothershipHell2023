using System.Collections.Generic;
using UnityEngine;

namespace Combat.AI
{
    public class SpawnReparentAll : ReactiveNext
    {
        public List<Transform> spawn;
        public bool onStart = false;
        public Transform target;

        private void Start()
        {
            if (onStart)
                React(null);
        }

        public override bool React(Outputs outputs)
        {
            for (int i = 0; i < spawn.Count; i++)
            {
                spawn[i].parent = transform.parent;
                spawn[i].gameObject.SetActive(true);
                spawn[i].position = target.position;
                spawn[i].rotation = Quaternion.identity;
            }
            spawn.Clear();
            return base.React(outputs);
        }
    }
}