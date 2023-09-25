using Combat.AI;
using UnityEngine;

public class RotateAround : ReactiveBase
{
    public bool autoRotate;
    public Transform rotateAround;
    public float rotationSpeed = 30;
    public ReactiveBase next;
    private void Update()
    {
        if (autoRotate)
            React(null);
    }

    public override bool React(Outputs outputs)
    {
        var dir = transform.position - rotateAround.position;
        var rotateBy = Quaternion.Euler(0, 0, rotationSpeed);
        var moveToPos = rotateAround.position + rotateBy * dir;
        transform.position = moveToPos;

        return lastResult = next ? next.React(outputs):true;
    }
}
