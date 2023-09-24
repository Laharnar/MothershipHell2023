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

        private void Start()
        {
            data.Add(A.hpP, 1f);
            data.Add(A.detection, new List<GameObject>());
            data.Add(A.motionType, R.move);
            unit = GetComponent<ReactiveUnit>();
            data.Add(A.unit, unit);
        }

        private void Update()
        {
            data[A.hpP] = hp.hpP;
            data[A.motionType] = R.inDirection;
            data[A.cast] = R.shoot;

            data[A.moveDirection] = Vector2.up * 0.9f;

            Outputs.UnitConvert(data, unit);

                rotationBase.React(data);
            if (hp.hpP <= 0)
            {
                Debug.Log("Destroy from low hp");
                Destroy(gameObject);
            }
        }
    }
}