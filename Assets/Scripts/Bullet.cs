using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Combat.AI
{

    public class Bullet : ReactiveBase
    {
        public string motion = "forward";
        Outputs data = new Outputs();
        ReactiveUnit unit;
        public int dmgOnHit = 1;
        public float life = 3;
        public GameSettings settings;
        public Group group;

        private void Start()
        {
            unit = GetComponent<ReactiveUnit>();
            Destroy(gameObject, life);
        }

        public void OnSpawn(ReactiveUnit source)
        {
            data[A.spawnBy] = source;
            data[A.group] = source.register.group;
            group = source.register.group;
        }

        private void Update()
        {
            data[A.motionType] = R.inDirection;
            data[A.moveDirection] = motion;
            Outputs.UnitConvert(data, unit);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var go = data.At<ReactiveUnit>(A.spawnBy);
            var target = collision.GetComponent<ReactiveUnit>();
            if ((go == null || collision.gameObject != go.gameObject)
                && target != null && target.isTargetable && target.register.group != group)
            { 
                if(settings)
                {
                    if (settings.dealDmg)
                    {
                        collision.GetComponent<Hp>()?.OnDmg(dmgOnHit);
                    }
                }
                Destroy(gameObject);
            }
        }

    }
}