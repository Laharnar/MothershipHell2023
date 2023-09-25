namespace Combat.AI
{
    public class Spawned : ReactiveBase
    {

        private Spawner spawner;
        private string spawnId;
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