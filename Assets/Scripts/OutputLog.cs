using System.Collections.Generic;

namespace Combat.AI
{
    public class OutputLog : ReactiveBase
    {
        public ReactiveBase next;

        public List<string> keys = new List<string>();
        public List<string> values = new List<string>();

        public override bool React(Outputs outputs)
        {
            keys.Clear();
            values.Clear();
            foreach (var kv in outputs)
            {
                keys.Add(kv.Key);
                values.Add(kv.Value?.ToString());
            }
            return next ? next.React(outputs) : true;
        }
    }
}