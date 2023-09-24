namespace Combat.AI
{
    public class SpawnPoint : ReactiveBase
    {
        public ReactiveBase next;

        public override bool React(Outputs outputs)
        {
            base.React(outputs);
            outputs[A.spawnPoint] = transform;

            return lastResult = !next || next.React(outputs);
        }
    }
}