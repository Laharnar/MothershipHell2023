using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Combat.AI
{

    public class Group : ReactiveBase
    {
        public List<ReactiveBase> targets = new List<ReactiveBase>();
        public List<Group> enemies = new List<Group>();
        public List<ReactiveBase> enemyTargets {
            get {
                var rb = new List<ReactiveBase>();
                foreach (var item in enemies)
                {
                    for (int i = 0; i < item.targets.Count; i++)
                    {
                        rb.Add(item.targets[i]);
                    }
                }
                return rb;
            }
        }

        internal Transform Find(string targeting, Vector3 position)
        {
            if (targeting == R.nearest)
                return Nearest(position);
            return First().transform;
        }

        internal ReactiveBase First()
        {
            return targets[0];
        }


        internal Group FirstEnemy()
        {
            return enemies[0];
        }

        internal Transform Nearest(Vector3 position)
        {
            float minDist = float.MaxValue;
            int min = -1;
            for (int i = 0; i < targets.Count; i++)
            {
                var dist = Vector2.Distance(targets[i].transform.position, position);
                if (dist < minDist)
                {
                    minDist = dist;
                    min = i;
                }
            }
            if (min == -1) return null;
            return targets[min].transform;
        }

        internal Transform Nearest(Vector3 position, List<ReactiveBase> bases)
        {
            float minDist = float.MaxValue;
            int min = -1;
            for (int i = 0; i < bases.Count; i++)
            {
                var dist = Vector2.Distance(bases[i].transform.position, position);
                if (dist < minDist)
                {
                    minDist = dist;
                    min = i;
                }
            }
            return bases[min].transform;
        }

        private void LateUpdate()
        {
            if (targets.Count != transform.childCount) {
                for (int i = targets.Count - 1; i >= 0; i--)
                {
                    if (targets[i].transform.parent != transform)
                    {
                        targets.RemoveAt(i);
                    }
                }
            }
        }

        internal Transform Random()
        {
            return targets[UnityEngine.Random.Range(0, targets.Count)].transform;
        }

        internal Transform FindEnemy(string targeting, Vector3 position)
        {
            if (targeting == R.nearest)
                return Nearest(position, enemyTargets);
            return First().transform;
        }

        internal void Remove(ReactiveBase register)
        {
            enemyTargets.Add(register);
            enemyTargets.Remove(register);
        }
    }
}