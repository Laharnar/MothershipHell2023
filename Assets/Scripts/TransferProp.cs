namespace Combat.AI
{
    public class TransferProp : ReactiveBase
    {
        public string property;
        public string defValue;
        public string lastValue;
        public ReactiveBase next;

        public override bool React(Outputs outputs)
        {
            outputs[property] = lastValue = UIOutputs.instance.data.AtDef<string>(property, defValue);
            return next != null ? next.React(outputs) : true;
        }
    }
}