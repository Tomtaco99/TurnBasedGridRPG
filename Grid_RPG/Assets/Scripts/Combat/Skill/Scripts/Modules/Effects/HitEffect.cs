using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Skills/Effects/New Hit Effect")]
public class HitEffect : Effect
{
    public int damage;
    public bool damagesAllTargets;
    [EnumToggleButtons]
    public Entity.EntityType entityTypeToDamage;
    public override void Anim()
    {
        throw new System.NotImplementedException();
    }

    public override void Sound()
    {
        throw new System.NotImplementedException();
    }

    public override void ApplyHit(TargetGroup target)
    {
        foreach (Tile.TileObject t in target.Tiles)
        {
            if (t != null)
            {
                if (t.TryDamageEntity(damage, entityTypeToDamage) && !damagesAllTargets)
                {
                    return;
                }
            }
        }
    }
}
