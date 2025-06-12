using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Effects/New Heal Effect")]

public class HealEffect : Effect
{
    public int healAmount = 2;
    public override void Anim()
    {
        throw new System.NotImplementedException();
    }

    public override void ApplyHit(TargetGroup target)
    {
        foreach (Tile.TileObject t in target.Tiles)
        {
            if (t != null && t.entity != null)
            {
                t.entity.HealEntity(healAmount);
            }
        }
    }

    public override void Sound()
    {
        throw new System.NotImplementedException();
    }
}
