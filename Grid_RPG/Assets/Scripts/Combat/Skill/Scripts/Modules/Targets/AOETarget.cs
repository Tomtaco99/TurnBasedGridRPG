using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Targets/AOE targets")]

public class AOETarget : Target
{
    public enum TargetOrigin
    {
        Unit,
        Tile
    }
    public enum EntitySkill
    {
        PlayerSkill,
        EnemySkill
    }

    [Range(1, 2)]
    public int AOERadious = 1;


    public TargetOrigin targetLocation;
    public EntitySkill entitySkill;

    public override TargetGroup GetTargets()
    {
        throw new System.NotImplementedException();
    }

    public override TargetGroup GetTargets(Vector3 worldPosition)
    {
        Tile targetGrid;
        if (entitySkill == EntitySkill.PlayerSkill)
        {
            targetGrid = GridManager.Instance.EnemyGrid;
        }
        else
        {
            targetGrid = GridManager.Instance.PlayerGrid;
        }
        Tile.TileObject targetTile = targetGrid.grid.GetGridObject(worldPosition);
        targetTile.ReturnTilePos(out int x, out int y);
        TargetGroup newTargetGroup = new TargetGroup();
        newTargetGroup.Tiles[0] = targetGrid.grid.GetGridObject(x, y);
        return new TargetGroup();
    }

    public override TargetGroup GetTargets(Tile position)
    {
        throw new System.NotImplementedException();
    }

    public override TargetGroup GetTargets(Movement character)
    {
        throw new System.NotImplementedException();
    }
}
