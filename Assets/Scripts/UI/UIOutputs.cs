using Combat.AI;
using UnityEngine;

public class UIOutputs : ReactiveBase
{
    public static UIOutputs instance;

    public Outputs data = new Outputs();
    public ReactiveBase next;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        instance = this;
    }

    private void Update()
    {
        React(data);
    }

    public override bool React(Outputs outputs)
    {
        return next ? next.React(outputs) : true;
    }
}
