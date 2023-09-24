
using UnityEngine;

namespace Combat.AI
{
    public class DestroySimple : ReactiveBase
    {
        public override bool React(Outputs outputs)
        {
            Debug.Log("destroy simple");
            Destroy(gameObject);
            return true;
        }
    }
}