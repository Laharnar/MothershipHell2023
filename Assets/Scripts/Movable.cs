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

        public override bool React(Outputs o)
        {
            if (o.Is(A.motion, R.inDirection))
            {
                if (o[A.moveDirection] is Vector2 v2)
                {
                    Move(v2);
                }
                else if (o[A.moveDirection] is string s)
                {
                    if (s == R.forward)
                        Move(Vector2.up);

                    if (s == R.left)
                        Move(Vector2.left);
                    if (s == R.right)
                        Move(Vector2.right);
                }
            }
            if (o.Is(A.motion, R.stop))
            {
                // do nothing
            }
            if (o.Is(A.motion, R.move))
            {
                if (!moveLock)
                {
                    o[A.movelock] = true;
                    MoveTo((Vector2)o[A.moveV2Pos], () => { moveLock = false; });
                }
            }
            return true;
        }

        public void Move(Vector2 dir, Action onDone = null)
        {
            transform.Translate(dir * 10 * Time.deltaTime * moveSpeed * quadratic * inverse / dir, Space.Self);
            onDone?.Invoke();
        }

        public void MoveTo(Vector2 pos, Action onDone = null)
        {
            var dir = (pos - (Vector2)transform.position);
            transform.Translate(dir * 10 * Time.deltaTime * (moveSpeed * quadratic + inverse / dir.magnitude), Space.Self);
            if (Vector2.Distance(pos, transform.position) < 0.1)
            {
                onDone?.Invoke();
            }
        }
    }
}