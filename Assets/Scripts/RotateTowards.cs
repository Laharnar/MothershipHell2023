using Combat.AI;
using UnityEngine;

public class RotateTowards : ReactiveNext
{
    public Transform rotateTowards;
    public float lerp;

    public override bool React(Outputs outputs)
    {
        var dir = rotateTowards.position - transform.position;
        var rot = Quaternion.Lerp(Quaternion.LookRotation(Vector3.forward, transform.up), Quaternion.LookRotation(Vector3.forward, dir), lerp);
        transform.rotation = rot;

        return base.React(outputs);
    }
}
