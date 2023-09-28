﻿using System;
using UnityEngine;

namespace Combat.AI
{

    public class Spawner : ReactiveBase
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

        public override bool React(Outputs outputs)
        {
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
                if (unit.Group == null) Debug.LogError("group wasnt assinged, incorrect setup");
                c.transform.parent = unit.Group.transform;
                c.transform.position = spawnPoint.position;
                c.transform.rotation = spawnPoint.rotation;
                c.gameObject.SetActive(true);

                if (c.TryGetComponent(out Bullet b))
                    b.OnSpawn(unit, this, key);

                if (c.TryGetComponent(out Spawned s))
                    s.OnSpawn(this, key);
                return lastResult = true;
            }
            Debug.LogError("no key 'bullet'" + this, this);
            return lastResult = false;
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