using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Combat.AI
{

    public class Spawner : ReactiveNext
    {

        public string spawnKey = "bullet";

        [Serializable]
        public class StringPref
        {
            public string name;
            public Transform pref;
            public ReactiveBase subunit;
            public int maxSpawned = 10;
            internal Pool pool = new Pool();
        }
        public StringPref[] prefs;
        public string lastValue;
        Group group;

        public override bool React(Outputs outputs)
        {
            if (group == null) group = GetComponentInParent<Group>();
            string key = outputs.AtDef<string>(A.spawnCode, spawnKey);
            lastValue = key;
            for (int i = 0; i < prefs.Length; i++)
            {
                if (prefs[i].name != key) continue;
                if (!prefs[i].pref) continue;
                if (!(!prefs[i].subunit || prefs[i].subunit.React(outputs))) continue;

                var spawnPoint = outputs.AtDef(A.spawnPoint, transform);
                var unit = outputs.At<ReactiveUnit>(A.unit);
                if (prefs[i].maxSpawned == 0) Debug.LogError("MAX SPAWNED  =  0 ", this);
                var c = prefs[i].pool.Pull(prefs[i].pref, prefs[i].maxSpawned);

                if (c == null) return lastResult = true;
                if (unit && unit.Group == null) Debug.LogError("group wasnt assinged, incorrect setup");
                if(unit != null)
                    group = unit.Group;
                c.transform.parent = group.transform;
                c.transform.position = spawnPoint.position;
                c.transform.rotation = spawnPoint.rotation;
                c.gameObject.SetActive(true);

                if (c.TryGetComponent(out Bullet b))
                    b.OnSpawn(unit, this, key);

                if (c.TryGetComponent(out Spawned s))
                    s.OnSpawn(this, key);
                return base.React(outputs);
            }
            Debug.LogError("no key 'bullet'" + this, this);
            return base.React(outputs);
        }

        internal void DestroyObj(string spawnId, GameObject gameObject)
        {
            for (int i = 0; i < prefs.Length; i++)
            {
                if (prefs[i].name == spawnId)
                {
                    prefs[i].pool.Destroy(gameObject);
                    break;
                }
            }
        }
    }
}