using System.Collections.Generic;

namespace Combat.AI
{

    public class Branch : ReactiveBase
    {
        public List<ReactiveBase> runAll = new List<ReactiveBase>();
        public bool disconnectedBranches = false;

        public override ReactiveBase Next(int i = 0)
        {
            return i < runAll.Count ? runAll[i] : null;
        }

        public override int Count => runAll.Count;

        public override bool React(Outputs outputs)
        {
            for (int i = 0; i < runAll.Count; i++)
            {
                if(disconnectedBranches)
                    runAll[i].React(outputs.Copy());
                else runAll[i].React(outputs);
            }
            return true;
        }
    }
}