namespace Combat.AI
{
    public class RunAll : Branch
    {
        public bool autoRun = false;

        private void Update()
        {
            if (autoRun)
                React(new Outputs());
        }
    }

    
}