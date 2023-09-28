using System.Collections.Generic;

namespace Combat.AI
{
    public class TransferProps : ReactiveBase
    {

        public List<string> property = new List<string>();
        public List<string> defValue = new List<string>();
        public string[] lastValue;
        public ReactiveBase next;

        private void Start()
        {
            lastValue = new string[property.Count];
        }

        public override bool React(Outputs outputs)
        {
            for (int i = 0; i < property.Count; i++)
            {
                outputs[property[i]] = lastValue[i] = UIOutputs.instance.data.AtDef(property[i], defValue[i]);
            }
            return next != null ? next.React(outputs) : true;
        }
    }
}