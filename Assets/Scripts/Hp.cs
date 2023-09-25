using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Combat.AI
{
    public class Hp : MonoBehaviour
    {
        public int maxHp = 10;
        public int hp = 0;
        public ReactiveBase onDmg;
        public ReactiveBase onDeath;

        public float hpP => (float)hp / maxHp;

        private void Awake()
        {
            hp = maxHp;
        }

        public void OnDmg( int positiveDmg)
        {
            hp -= positiveDmg;
            onDmg?.React(null);
            if (hp <= 0)
            {
                onDeath?.React(null);
            }
        }
    }
}