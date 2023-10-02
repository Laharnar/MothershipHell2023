using Combat.AI;

public class Resettable : ReactiveNext
{
    public FollowObject[] resettable;

    public override bool React(Outputs outputs)
    {
        foreach (var item in resettable)
        {
            item.FullReset();
        }
        return base.React(outputs);
    }
}
