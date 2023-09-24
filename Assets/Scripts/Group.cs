using System.Collections.Generic;

namespace Combat.AI
{
    public class Group : ReactiveBase
    {
        public List<ReactiveBase> targets = new List<ReactiveBase>();
    }
}