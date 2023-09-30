using System;
using UnityEngine;

namespace Combat.AI
{

    public class Register : ReactiveBase
    {
        [Header("no react behaviour")]
        public string note;
        public Group group;
        public bool isTargetable = true;

        private void Start()
        {
            if (group == null) group = GetComponentInParent<Group>();
            group.targets.Add(this);
        }

        private void OnValidate()
        {
            if (group == null) group = GetComponentInParent<Group>();
        }

        private void OnDestroy()
        {
            group.targets.Remove(this);
        }

        internal void Reload(Group groupNew)
        {
            this.group = groupNew;
        }
    }
}