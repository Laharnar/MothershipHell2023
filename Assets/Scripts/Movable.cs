using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.AI
{

    public class Movable : ReactiveBase
    {
        internal bool moveLock;
        public float moveSpeed = 1f;
        public float quadratic = 1f;
        public float inverse = 0f;

        public string autoMode;
        public Vector2 direction;
        public Vector2 position;

        public Vector2 activeDirection;
        public Vector2 activePosition;

        private void Start()
        {
            activeDirection = direction;
            activePosition = position;
        }

        public override void OverrideParams(Outputs transfer)
        {
            if (transfer.ContainsKey(A.moveDirection) && transfer[A.moveDirection] is Vector2)
                activeDirection = transfer.At<Vector2>(A.moveDirection);
            if (transfer.ContainsKey(A.moveV2Pos) && transfer[A.moveV2Pos] is Vector2)
                activePosition = transfer.At<Vector2>(A.moveV2Pos);
        }

        public override bool React(Outputs o)
        {
            if (o.IsC(A.motion, R.inDirection))
            {
                if (o.ContainsMultitype(A.moveDirection))
                {
                    Vector2 v2 = Vector2.zero;
                    if (o[A.moveDirection] is Vector2 v1)
                    {
                        v2 = v1;
                    }
                    else if (o[A.moveDirection] is string s)
                    {
                        if (s == R.forward)
                            v2 = Vector2.up;

                        if (s == R.left)
                            v2 = Vector2.left;
                        if (s == R.right)
                            v2 = Vector2.right;
                    }
                    Move(v2);
                }
                else
                {
                    Move(activeDirection);
                }
            }else
            if (o.IsC(A.motion, R.stop))
            {
                // do nothing
            }else 
            if (o.IsC(A.motion, R.move))
            {
                if (!moveLock)
                {
                    if (o.ContainsMultitype(A.moveV2Pos))
                    {
                        o[A.movelock] = true;
                        MoveTo((Vector2)o[A.moveV2Pos], () => { moveLock = false; });
                    }
                    else
                    {
                        o[A.movelock] = true;
                        MoveTo(activePosition, () => { moveLock = false; });
                    }
                }
            }
            else
            {
                o[A.motion] = autoMode;
                if (autoMode == R.inDirection || autoMode == R.move)
                    React(o);
                else Debug.LogError("invalid setup "+autoMode, this);
            }
            return lastResult = base.React(o);
        }

        public void Move(Vector2 dir, Action onDone = null)
        {
            if(dir != Vector2.zero)
            transform.Translate(dir.normalized * 10 * Time.deltaTime * (moveSpeed * quadratic + inverse / dir.magnitude), Space.Self);
            onDone?.Invoke();
        }

        public void MoveTo(Vector2 pos, Action onDone = null)
        {
            var dir = (pos - (Vector2)transform.position);
            if(dir != Vector2.zero)
            transform.Translate(dir.normalized * 10 * Time.deltaTime * (moveSpeed * quadratic + inverse / dir.magnitude), Space.Self);
            if (Vector2.Distance(pos, transform.position) < 0.1)
            {
                onDone?.Invoke();
            }
        }
    }
}