using System.Collections.Generic;
using UnityEngine;

namespace Combat.AI
{

    public class ReactiveLogic : ReactiveBase
    {
        Outputs data = new Outputs();

        private void Start()
        {
            data.Add(A.hpP, 1f);
            data.Add(A.detection, new List<GameObject>());
            data.Add(A.motionType, R.move);
        }

        private void Update()
        {
            ChillAndAvoidDog(data, GetComponent<ReactiveUnit>());
            Debug.Log(data.Log());
        }

        public void ChillAndAvoidDog(Outputs data, ReactiveUnit unit)
        {
            if (DefaultHpNode(data))
            {
                DogInWay(data);
            }
            else
            {
                LowHpFlee(data);
            }
            ToActions(data);
            Outputs.UnitConvert(data, unit);
            lastResult = true;
        }

        public void ToActions(Outputs data)
        {
            if (data.Is(A.act, R.flee))
            {
                data[A.cast] = R.shoot;
                data[A.motionType] = R.move;
            }
            else if (data.Is(A.act, R.encircle))
            {
                data[A.cast] = R.none;
                data[A.motionType] = R.encircle;
            }
            else if (data.Is(A.act, R.fight))
            {
                data[A.cast] = R.shoot;
            }
        }

        public bool LowHpFlee(Outputs data, float lowHpPerc = 0.2f)
        {
            if (data.More(A.hpP, lowHpPerc))
                return false;

            data.Add(A.act, R.flee);
            data[A.castTarget] = R.nearest;
            return true;
        }

        public bool DefaultHpNode(Outputs data, float highHp = 0.4f)
        {
            data[A.act] = R.fight;
            data[A.castTarget] = R.nearest;
            if (data.More(A.hpP, highHp))
                return true;
            return false;
        }

        public void DogInWay(Outputs data)
        {
            if(data.At(A.detection, out List<GameObject> detected).Count > 0)
            if (detected[0]!= null && detected[0].transform.name == C.dog)
            {
                data[A.act] = R.encircle;
                data[A.moveV2Pos] = (Vector2)detected[0].transform.position;
                data[A.castTarget] = R.none;
            }
        }

    }
}