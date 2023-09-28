using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.AI
{
    [Serializable]
    public class Pool
    {
        public List<GameObject> pool = new List<GameObject>();
        public List<GameObject> tracked = new List<GameObject>();

        public GameObject Pull(Transform prefab, int max)
        {
            if (pool.Count > 0)
            {
                var obj = pool[0];
                pool.RemoveAt(0);
                tracked.Add(obj);
                return obj;
            }
            else
            {
                if (tracked.Count > max)
                    return null;
                var obj = GameObject.Instantiate(prefab);
                tracked.Add(obj.gameObject);
                return obj.gameObject;
            }
        }

        internal void Destroy(GameObject gameObject)
        {
            tracked.Remove(gameObject);
            pool.Add(gameObject);
            //gameObject.transform.position += new Vector3(10000, 0, 0);
            gameObject.SetActive(false);
        }
    }
}