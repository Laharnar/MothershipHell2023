using UnityEngine;

namespace Combat.AI
{
    public class OnHit : MonoBehaviour
    {
        public int dmgOnHit = 1;
        public GameSettings settings;
        public Group group;
        public bool destroySelfOnHit = false;

        private void OnValidate()
        {
            if (group == null)
                group = GetComponentInParent<Group>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.GetComponent<ReactiveUnit>();
            if (target != null && target.isTargetable && target.register.group != group)
            {
                if (!settings || settings.dealDmg)
                {
                    collision.GetComponent<Hp>()?.OnDmg(dmgOnHit);
                }
                if(destroySelfOnHit)
                Destroy(gameObject);
            }else if (collision.TryGetComponent<Hp>(out Hp hp))
            {
                hp.OnDmg(dmgOnHit);
            }
        }

    }
}