using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Combat.AI
{

    public class Bullet : ReactiveBase
    {
        public string motion = "forward";
        Outputs data = new Outputs();
        ReactiveUnit self;
        public int dmgOnHit = 1;
        public float life = 3;
        public GameSettings settings;
        public Group group;
        private Spawner spawner;
        private string spawnId;
        private float time;

        private void Start()
        {
            self = GetComponent<ReactiveUnit>();
        }

        public void OnSpawn(ReactiveUnit source, Spawner spawner, string spawnId)
        {
            if (source)
            {
                data[A.spawnBy] = source;
                data[A.group] = source.register.group;
                group = source.register.group;
            } else {
                group = GetComponentInParent<Group>();
                data[A.group] = group;
            }
            time = 0;
            this.spawner = spawner;
            this.spawnId = spawnId;
        }

        private void Update()
        {
            if (time >= life)
            {
                if (data.Exists(A.spawnBy, out ReactiveUnit u))
                    spawner.DestroyObj(spawnId, gameObject);
            }
            time += Time.deltaTime;
            data[A.motion] = R.inDirection;
            data[A.moveDirection] = motion;
            Outputs.ApplyToUnit(data, self);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var go = data.At<ReactiveUnit>(A.spawnBy);
            var target = collision.GetComponent<ReactiveUnit>();
            Action onCollide = () =>
            {
                if (settings)
                {
                    if (settings.dealDmg)
                    {
                        collision.GetComponent<Hp>()?.OnDmg(dmgOnHit);
                    }
                }
                if (spawner != null) spawner.DestroyObj(spawnId, gameObject);
                else Destroy(gameObject);
            };
            if ((go == null || collision.gameObject != go.gameObject)
                && (target != null && (target.isTargetable && target.register.group != group)))
            {
                onCollide();
            }
            else if (target == null) {
                var reg = collision.GetComponentInParent<Register>();
                if (reg && reg.isTargetable && reg.group != group)
                {
                    onCollide();
                }
            }
        }
    }
}