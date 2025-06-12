using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Targets/Multiple Targets")]
public class MultipleTileTarget : Target
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

    [Range(2, 5)]
    public int numberOfTiles = 3;


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
        newTargetGroup.Tiles = new Tile.TileObject[numberOfTiles];
        int xGrid = x;
        int yGrid = y;
        if (area == Area.Row)
        {
            for (int i = 0; i < numberOfTiles; i++)
            {
                if (xGrid > 4)
                    break;
                newTargetGroup.Tiles[i] = targetGrid.grid.GetGridObject(xGrid, y);
                xGrid++;
            }
        }
        else
        {
            for (int i = 0; i < numberOfTiles; i++)
            {
                if (yGrid > 4)
                    break;
                newTargetGroup.Tiles[i] = targetGrid.grid.GetGridObject(x, yGrid);
                yGrid++;
            }
        }
        return newTargetGroup;
    }

    public override TargetGroup GetTargets(Movement character)
    {
        throw new System.NotImplementedException();
    }

    public override TargetGroup GetTargets(Tile position)
    {
        throw new System.NotImplementedException();
    }


}
