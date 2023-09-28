using UnityEngine;

namespace Combat.AI
{
    public class WASD : ReactiveNext
    {
        public float moveSpeed = 10f;

        public override bool React(Outputs outputs)
        {
            var dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.position += dir *Time.deltaTime * moveSpeed;
            return base.React(outputs);
        }
    }
}