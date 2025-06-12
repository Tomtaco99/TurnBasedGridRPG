using UnityEngine;
public abstract class Target : Module
{
    public abstract TargetGroup GetTargets();

    public abstract TargetGroup GetTargets(Vector3 worldPosition);

    public abstract TargetGroup GetTargets(Movement character);

    public abstract TargetGroup GetTargets(Tile position);

}
