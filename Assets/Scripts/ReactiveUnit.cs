using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Combat.AI
{
    public class Outputs : Dictionary<string, object>
    {
        public bool warn = true;
        public new object this[string s] {
            get
            {
                if (!ContainsKey(s))
                {
                    Add(s, null);
                    if(warn)
                        Debug.Log("forced null value for "+s);
                }
                return base[s];
            }
            set {
                if (!ContainsKey(s))
                    Add(s, value);
                else base[s] = value;
            }
        }

        public T At<T>(string s) => (T)this[s];
        public bool Exists <T>(string s, out T val) => (val = (T)this[s]) != null;

        public T At<T>(string s, out T outp) => outp = (T)this[s];
        
        public bool Is(string s, string t) => t == (string)this[s];

        /// <summary>
        /// is stat s > value t
        /// </summary>
        public bool More(string s, float t) => (float)this[s] > t;

        /// <summary>
        /// is stat s < value t
        /// </summary>
        public bool Less(string s, float t) => (float)this[s] < t;

        public string Log()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this)
            {
                sb.Append(item.Key);
                sb.Append(" --> ");
                sb.Append(item.Value);
                sb.Append(";\n");
            }
            return sb.ToString();
        }

        public static void ApplyToUnit(Outputs o, ReactiveUnit unit)
        {
            unit.SetOutputs(o);

            if (o.Is(A.motion, R.inDirection))
            {
                if (o[A.moveDirection] is Vector2 v2)
                {
                    unit.Move(v2);
                }
                else if (o[A.moveDirection] is string s)
                {
                    if (s == R.forward)
                        unit.Move(Vector2.up);

                    if (s == R.left)
                        unit.Move(Vector2.left);
                    if (s == R.right)
                        unit.Move(Vector2.right);
                }
            }
            if(o.Is(A.motion, R.stop))
            {
                // do nothing
            }

            if (o.Is(A.motion, R.encircle))
            {
                if (!unit.moveLock)
                {
                    unit.moveLock = true;
                    unit.MoveTo(
                        (Vector2)o[A.moveV2Pos] + Vector2.left, () => {
                            unit.MoveTo(
                                (Vector2)o[A.moveV2Pos] + Vector2.up,
                                () => { unit.moveLock = false;
                                    o[A.motion] = R.move;
                                    o[A.act] = R.fight;
                                    o.At<List<GameObject>>(A.detection).Clear();
                                    });
                        });
                }
            }
            o.warn = false;
            if (o.Is(A.cast, R.shoot))
            {
                if (!unit.castLock)
                {
                    unit.castLock = true;
                    unit.Shoot(() => { unit.castLock = false; });
                }
            }
            else if (o.Is(A.motion, R.move))
            {
                if (!unit.moveLock)
                {
                    o[A.movelock] = true;
                    unit.MoveTo((Vector2)o[A.moveV2Pos], () => { unit.moveLock = false; });
                }
            }
            o.warn = true;
        }

        internal T AtDef<T>(string v, T defValue)
        {
            var t = At<T>(v);
            if (t == null)
                this[v] = defValue;
            return t != null ? t : defValue;
        }
    }

    public class ReactiveUnit : MonoBehaviour
    {
        internal Outputs output = new Outputs();
        internal bool moveLock;
        internal bool castLock;
        private Vector2? moveTo;
        private Action moveToOnDone;
        public float moveSpeed = 1f;
        public ReactiveBase shooter;
        public Register register;
        public bool isTargetable = true;

        public Group Group => register!= null ? register.group : null;

        private void Start()
        {
            if(register == null)
            register = GetComponent<Register>();
        }

        private void Update()
        {
            if (moveTo != null)
                MoveTo(moveTo.Value, moveToOnDone);
        }

        public void SetOutputs(Outputs outp)
        {
            this.output = outp;
            outp[A.unit] = this;
            if(register == null)register = GetComponentInParent<Register>();
        }

        public void Shoot(Action onDone = null)
        {
            if(register == null)register = GetComponentInParent<Register>();
            shooter.React(output);
            onDone?.Invoke();
        }

        public void Move(Vector2 dir, Action onDone = null)
        {
            // Implement the logic for moving the unit using the provided vector
            transform.Translate(dir * 10 * Time.deltaTime * moveSpeed, Space.Self);
            onDone?.Invoke();
        }

        public void MoveTo(Vector2 pos, Action onDone = null, bool quadratic = false)
        {
            moveTo = pos;
            moveToOnDone = onDone;
            // Implement the logic for moving the unit using the provided vector
            var dir = (pos - (Vector2)transform.position);
            if (!quadratic) dir = dir.normalized;
            transform.Translate(dir * 10 * Time.deltaTime  * moveSpeed, Space.Self);
            if (Vector2.Distance(pos, transform.position) < 0.1)
            {
                moveTo = null;
                onDone?.Invoke();
            }
        }

        
    }


    static class A // attributes
    {
        public const string act = "act";
        // who to target : nearest, none
        public const string castTarget = "castTarget";
        public const string castlock = "castlock";
        public const string movelock = "movelock";
        public const string hpP = "hp%";
        public const string motion = "motion";
        /// <summary>
        /// set to R.shoot/R.none
        /// </summary>
        public const string cast = "cast";
        /// <summary>
        /// Vector2 move towards pos
        /// </summary>
        public const string moveV2Pos = "moveTarget";
        public const string moveDirection = "moveDirection";
        public const string spawnPoint = "spawnPoint";
        public const string spawnBy = "spawnBy";
        public const string detection = "detection"; // list of game objects
        public const string unit = "unit";
        public const string group = "group";
    }

    static class R // reactions
    {
        public const string encircle = "encircle";
        public const string move= "move";
        public const string stop = "stop";
        public const string nearest = "nearest";
        public const string shoot = "shoot";
        public const string fight= "fight";
        public const string flee= "flee";
        public const string none = "none";
        public const string forward = "forward";
        public const string inDirection = "direction";
        public const string random = "random";
        public const string left = "left";
        public const string right = "right";
    }

    static class C // characters
    {
        public const string dog = "dog";
    }

    static class Positions
    {
        public const string inFront = "in front";
    }
}