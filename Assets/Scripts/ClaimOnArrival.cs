using Combat.AI;
using UnityEngine;

public class ClaimOnArrival : ReactiveNext
{
    public Register claimIntoReg;
    public Group claimInto;
    public float claimDistance = 0.3f;
    public FollowObject resettable;

    private void OnValidate()
    {
        if (claimIntoReg == null) claimIntoReg = GetComponent<Register>();
    }

    public override bool React(Outputs outputs)
    {
        if(outputs.Exists<Transform>(A.claimTarget, out Transform t))
        {
            if (Vector2.Distance(transform.position, t.position) < claimDistance)
            {
                if (t.TryGetComponent<Claimable>(out Claimable cl))
                {
                    if (cl.claimed && cl.claimedBy != transform && resettable)
                    {
                        Debug.Log("reset");
                        resettable.target = null;
                    }
                    else
                    {
                        outputs[A.claimTarget] = (claimIntoReg ? claimIntoReg.group : claimInto).transform;
                        outputs[A.claimSource] = transform;
                        cl.React(outputs);
                    }
                }
            }
        }
        return base.React(outputs);
    }
}
