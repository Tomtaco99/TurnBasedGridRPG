using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Targets/Predefined Single Target")]
public class PredefinedSingleTarget : Target
{
    public enum EntitySkill
    {
        PlayerSkill,
        EnemySkill
    }
    public EntitySkill entitySkill;

    [Range(-9, 9)]
    public int horizontalOffset = 1;
    [Range(-9, 9)]
    public int VerticalOffset = 0;
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
        CombatManager.Instance.actionList[0].ReturnPosition(out int myX, out int myY);
        int xN = myX + horizontalOffset;
        int yN = Mathf.Clamp(myY + VerticalOffset, 0, 4);
        if (xN > 4)
        {
            xN = xN - 5;
        }
        else if (xN < 0)
        {
            xN = xN + 5;
        }
        else
        {
            if (entitySkill == EntitySkill.PlayerSkill)
            {
                targetGrid = GridManager.Instance.PlayerGrid;
            }
            else
            {
                targetGrid = GridManager.Instance.EnemyGrid;
            }
        }
        Tile.TileObject targetTile = targetGrid.grid.GetGridObject(xN, yN);
        TargetGroup newTargetGroup = new TargetGroup();
        newTargetGroup.Tiles[0] = targetTile;
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
