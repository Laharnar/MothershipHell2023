using UnityEngine;

namespace Combat.AI
{
    public class OnHitPeriodicDmg:MonoBehaviour
    {
        public int dmgOnHit = 1;
        public GameSettings settings;
        public Group group;
        public bool destroySelfOnHit = false;
        public float timer = 2;
        float time;

        private void OnValidate()
        {
            if (group == null)
                group = GetComponentInParent<Group>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsReady()) return;

            var target = collision.GetComponent<ReactiveUnit>();
            if (target != null && target.isTargetable && target.register.group != group)
            {
                if (!settings || settings.dealDmg)
                {
                    time = Time.deltaTime + timer;
                    collision.GetComponent<Hp>()?.OnDmg(dmgOnHit);
                }
                if (destroySelfOnHit)
                    Destroy(gameObject);
            }
            else if (collision.TryGetComponent<Hp>(out Hp hp))
            {
                time = Time.deltaTime + timer;
                hp.OnDmg(dmgOnHit);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            OnTriggerEnter2D(collision);
        }

        private bool IsReady()
        {
            return Time.time > time;
        }
    }
}