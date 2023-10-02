using UnityEngine;

namespace Combat.AI
{
    public class OnHit : MonoBehaviour
    {
        public int dmgOnHit = 1;
        public GameSettings settings;
        public Group group;
        public bool destroySelfOnHit = false;
        public ReactiveBase onHit;
        public bool filters = true;

        private void OnValidate()
        {
            if (group == null)
                group = GetComponentInParent<Group>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            bool hit = false;
            var target = collision.GetComponent<ReactiveUnit>();
            if (filters && target != null && target.isTargetable && target.register.group != group)
            {
                if (!settings || settings.dealDmg)
                {
                    collision.GetComponent<Hp>()?.OnDmg(dmgOnHit);
                    onHit?.React(new Outputs());
                }
                hit = true;
            } else if (collision.TryGetComponent<Hp>(out Hp hp))
            {
                hit = true;
                hp.OnDmg(dmgOnHit);
                onHit?.React(new Outputs());
            } else
            {
                hit = true;
                onHit?.React(new Outputs());
            }
            if (hit)
            {
                if (destroySelfOnHit)
                    Destroy(gameObject);
            }

        }

    }
}