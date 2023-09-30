namespace Combat.AI
{
    public class RunAll : Branch
    {
        public bool autoRun = false;

        public override string Msg => "auto:"+autoRun;

        private void Update()
        {
            if (autoRun)
                React(new Outputs());
        }
    }

    
}