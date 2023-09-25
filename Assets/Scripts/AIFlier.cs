using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Combat.AI
{

    public class AIFlier : ReactiveBase
    {
        Outputs data = new Outputs();
        public Hp hp;
        private ReactiveUnit unit;
        public ReactiveBase rotationBase;

        public string firstMovement = R.inDirection;
        public string firstTarget = R.none;

        private void Start()
        {
            data.Add(A.hpP, 1f);
            data.Add(A.detection, new List<GameObject>());
            data.Add(A.motion, R.move);
            unit = GetComponent<ReactiveUnit>();
            data.Add(A.unit, unit);
        }

        private void Update()
        {
            data[A.hpP] = hp.hpP;
            data[A.motion] = firstMovement;
            data[A.cast] = R.shoot;
            data[A.castTarget] = firstTarget;

            data[A.moveDirection] = Vector2.up * 0.9f;

            rotationBase?.React(data);
            Outputs.ApplyToUnit(data, unit);

            if (hp.hpP <= 0 && hp.onDeath == null)
            {
                Destroy(gameObject);
            }
        }
    }
}