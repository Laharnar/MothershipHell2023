using System.Transactions;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Combat.AI
{
    public class Hp : ReactiveBase
    {
        public int maxHp = 10;
        public int hp = 0;

        public ReactiveBase onDmg;
        public ReactiveBase onDeath;

        public override ReactiveBase Next(int i = 0)
        {
            if (i == 0) return onDmg;
            return onDeath;
        }
        public override int Count => 2;

        public float hpP => (float)hp / maxHp;

        private void Awake()
        {
            hp = maxHp;
        }

        public void OnDmg( int positiveDmg)
        {
            hp -= positiveDmg;
            onDmg?.React(new Outputs());
            if (hp <= 0)
            {
                onDeath?.React(null);
            }
        }
    }
}