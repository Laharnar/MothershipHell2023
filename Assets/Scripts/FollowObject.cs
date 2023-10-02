using Combat.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowObject : ReactiveBase
{
    public bool autoreact = false;
    public Group findTarget;
    public string targeting = "nearest";
    public Transform target;
    public bool position = true;
    public bool disableZ = true;
    public bool rotation;
    public bool directional = false;
    public float dirSpeed = 1;
    public bool lerpPosition = false;
    public bool lerpRotation = false;
    public float lerpAmount = 0.4f;
    public float overshot = 0;
    public float minDist = 0;

    public ReactiveBase next;

    public override ReactiveBase Next(int i = 0)
    {
        return next;
    }



    private void OnValidate()
    {
        if(isActiveAndEnabled)
        if (findTarget == null)
            findTarget = GetComponentInParent<Register>().group.enemies[0];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, minDist);
    }

    public override bool React(Outputs outputs)
    {
        if (target == null && findTarget != null)
            target = findTarget.Find(targeting, transform.position);

        outputs[A.claimTarget] = target;
        if (target != null &&
            Vector2.Distance(transform.position, target.position) > minDist)
        {
            var pos = transform.position;
            var overShotPos = target.position;
            overShotPos += target.up * overshot;
            var dir = target.position - transform.position;
            if (lerpPosition && position) pos = Vector3.Lerp(transform.position, overShotPos, lerpAmount);
            else if (directional && position)
                pos = transform.position + dir.normalized * 0.8f * dirSpeed * Time.deltaTime;
            else if (position) pos = target.position;

            if (disableZ)
                pos.z = transform.position.z;

            transform.position = pos;

            var rot = transform.rotation;
            if (rotation && lerpRotation) rot= Quaternion.Lerp(transform.rotation, target.rotation, lerpAmount);
            if (rotation) rot = target.rotation;
            transform.rotation = rot;
        }
        return lastResult = next ? next.React(outputs) : true;
    }

    void Update()
    {
        if (autoreact)
        {
            React(new Outputs());
        }
    }

    internal void FullReset()
    {
        target = null;
    }
}
