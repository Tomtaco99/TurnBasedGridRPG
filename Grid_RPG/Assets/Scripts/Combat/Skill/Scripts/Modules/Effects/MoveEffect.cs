using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Effects/New Move Effect")]
public class MoveEffect : Effect
{
    public enum KnockbackDireciton
    {
        Left,
        Right,
        Up,
        Down
    }

    public KnockbackDireciton knockbackDireciton;
    [Range(1, 5)]
    public int knockbackDistance;
    public override void Anim()
    {
        throw new System.NotImplementedException();
    }

    public override void ApplyHit(TargetGroup target)
    {
        foreach (Tile.TileObject t in target.Tiles)
        {
            if (t != null && t.EntityMovement != null)
            {
                t.ReturnTilePos(out int x, out int y);
                switch (knockbackDireciton)
                {
                    case KnockbackDireciton.Right:
                        int newX = Mathf.Clamp(x + knockbackDistance, 0, 4);
                        t.EntityMovement.SetNewEntityPosition(newX, y);
                        break;
                    case KnockbackDireciton.Left:
                        newX = Mathf.Clamp(x - knockbackDistance, 0, 4);
                        t.EntityMovement.SetNewEntityPosition(newX, y);
                        break;
                    case KnockbackDireciton.Up:
                        int newY = Mathf.Clamp(y + knockbackDistance, 0, 4);
                        t.EntityMovement.SetNewEntityPosition(x, newY);
                        break;
                    case KnockbackDireciton.Down:
                        newY = Mathf.Clamp(y - knockbackDistance, 0, 4);
                        t.EntityMovement.SetNewEntityPosition(x, newY);
                        break;
                    default:
                        break;
                }
            }
        }
    }


    public override void Sound()
    {
        throw new System.NotImplementedException();
    }
}
