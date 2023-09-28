using UnityEngine;

namespace Combat.AI
{
    public class Spawned : ReactiveBase
    {
        [Header("logs/no react")]
        public Spawner spawner;
        public string spawnId;
        //public Spawned spawned;
        
        public void OnSpawn(Spawner spawner, string spawnId)
        {
            this.spawner = spawner;
            this.spawnId = spawnId;
        }

        public void Destroy()
        {
            if (spawner)
                spawner.DestroyObj(spawnId, gameObject);
            else Destroy(gameObject);
        }
    }
}