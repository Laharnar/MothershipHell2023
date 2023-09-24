using System;
using UnityEngine;

namespace Combat.AI
{

    public class Spawner : ReactiveBase
    {

        [Serializable]
        public class StringPref
        {
            public string name;
            public Transform pref;
            public ReactiveBase subunit;
        }
        public StringPref[] prefs;

        public override bool React(Outputs outputs)
        {
            string key = "bullet";
            for (int i = 0; i < prefs.Length; i++)
            {
                if (prefs[i].name != key) continue;
                if (!prefs[i].pref) continue;
                if (!(!prefs[i].subunit || prefs[i].subunit.React(outputs))) continue;

                var spawnPoint = outputs.AtDef(A.spawnPoint, transform);
                var unit = outputs.At<ReactiveUnit>(A.unit);
                var c = Instantiate(prefs[i].pref, spawnPoint.position, spawnPoint.rotation, unit.transform.root);
                c.gameObject.SetActive(true);

                if (c.TryGetComponent(out Bullet b))
                    b.OnSpawn(unit);
                return lastResult = true;
            }
            Debug.Log("no key 'bullet'");
            return lastResult = false;
        }
    }
}