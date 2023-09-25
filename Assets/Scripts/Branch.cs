using System.Collections.Generic;

namespace Combat.AI
{
    public class Branch : ReactiveBase
    {
        public List<ReactiveBase> runAll = new List<ReactiveBase>();

        public override bool React(Outputs outputs)
        {
            for (int i = 0; i < runAll.Count; i++)
            {
                runAll[i].React(outputs);
            }
            return true;
        }
    }
}