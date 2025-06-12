using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Targets/Colum or Row targets")]
public class ColumnOrRow : Target
{
    public enum Area
    {
        Row,
        Colum
    }

    public enum EntitySkill
    {
        PlayerSkill,
        EnemySkill
    }

    public Area area;
    public EntitySkill entitySkill;


    public override TargetGroup GetTargets()
    {
        throw new System.NotImplementedException();
    }

    public override TargetGroup GetTargets(Movement character)
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
        TargetGroup newTargetGroup = new TargetGroup();
        if (targetTile == null)
        {
            return newTargetGroup;
        }
        targetTile.ReturnTilePos(out int x, out int y);

        newTargetGroup.Tiles = new Tile.TileObject[5];
        if (area == Area.Row)
        {
            for (int i = 0; i < 5; i++)
            {
                newTargetGroup.Tiles[i] = targetGrid.grid.GetGridObject(i, y);
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                newTargetGroup.Tiles[i] = targetGrid.grid.GetGridObject(x, i);
            }
        }
        return newTargetGroup;
    }

    public override TargetGroup GetTargets(Tile position)
    {
        throw new System.NotImplementedException();
    }
}
