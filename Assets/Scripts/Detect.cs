using Combat.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public ReactiveUnit self;
    public Alliance alliance;
    public List<ReactiveUnit> units;
    public Group AllyGroup => self.Group;

    public enum Alliance
    {
        Any,
        Ally,
        Enemy
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject != self && collision.TryGetComponent(out ReactiveUnit u)
            && IsRightAlliance(alliance, u))
            units.Add(u);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != self && collision.TryGetComponent(out ReactiveUnit u)
            && IsRightAlliance(alliance, u))
            units.Remove(u);
    }

    private bool IsRightAlliance(Alliance alliance, ReactiveUnit u)
    {
        return alliance == Alliance.Any || (alliance == Alliance.Ally && u.Group == AllyGroup) || (alliance == Alliance.Enemy && u.Group != AllyGroup);
    }
}
