using UnityEngine;

namespace Combat.AI
{
    public class Register : ReactiveBase
    {
        [Header("no react behaviour")]
        public string note;
        public Group group;

        private void Start()
        {
            if (group == null) group = GetComponentInParent<Group>();
            group.targets.Add(this);
        }

        private void OnDestroy()
        {
            group.targets.Remove(this);
        }
    }
}