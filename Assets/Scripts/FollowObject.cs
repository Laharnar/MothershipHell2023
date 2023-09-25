using Combat.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : ReactiveBase
{
    public Group findTarget;
    public Transform target;
    public bool position = true;
    public bool disableZ = true;
    public bool rotation;
    public bool lerpPosition = false;
    public bool lerpRotation = false;
    public float lerpAmount = 0.4f;
    public float overshot = 0;

    public bool autoreact = true;
    public ReactiveBase next;

    public override bool React(Outputs outputs)
    {
        if (target == null && findTarget != null)
            target = findTarget.First().transform;
        
        if (target != null)
        {
            var pos = transform.position;
            var overShotPos = target.position;
            overShotPos += target.up * overshot;
            if (lerpPosition && position) pos = Vector3.Lerp(transform.position, overShotPos, lerpAmount);
            else if (position) pos = target.position;

            if (disableZ)
                pos.z = transform.position.z;

            transform.position = pos;

            var rot = transform.rotation;
            if (rotation && lerpRotation) rot= Quaternion.Lerp(transform.rotation, target.rotation, lerpAmount);
            if (rotation) rot = target.rotation;
            transform.rotation = rot;
        }
        return next ? next.React(outputs) : true;
    }

    void Update()
    {
        if (autoreact)
        {
            React(new Outputs());
        }
    }
}
