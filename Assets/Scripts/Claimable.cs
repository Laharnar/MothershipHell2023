using Combat.AI;
using System.Collections.Generic;
using UnityEngine;

public class Claimable : ReactiveNext
{
    public bool claimed = false;
    [Header("Call react once to link it's parent to claimTarget")]
    public Transform linked;
    public Register register;
    public Transform claimedBy;
    public List<Claimable> alsoClaim = new List<Claimable>();

    public override bool React(Outputs outputs)
    {
        if (!claimed)
        {
            claimed = true;

            claimedBy = outputs.At<Transform>(A.claimSource);

            var groupTo = outputs.At<Transform>(A.claimTarget);
            MoveToNewGroup(groupTo);
            for (int i = 0; i < alsoClaim.Count; i++)
            {
                alsoClaim[i].React(outputs);
            }
        }
        return base.React(outputs);
    }

    private void MoveToNewGroup(Transform groupTo)
    {
        linked.parent = groupTo.transform;
        transform.parent = groupTo.transform;
        register?.Reload(groupTo.GetComponent<Group>());
    }
}
