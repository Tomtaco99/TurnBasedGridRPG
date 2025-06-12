using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Skills/Targets/Predefined Multiple Targets")]
public class PredefinedMultipleTarget : Target
{
    public enum Area
    {
        Row,
        Colum
    }

    public enum TargetUnit
    {
        PlayerUnits,
        EnemyUnits
    }
    public enum EntitySkill
    {
        PlayerSkill,
        EnemySkill
    }
    public Area area;
    public TargetUnit targetUnit;
    public EntitySkill entitySkill;

    [Range(2, 5)]
    public int numberOfTiles = 3;
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
        Tile targetGrid = GridManager.Instance.EnemyGrid;
        bool otherGrid = false;

        CombatManager.Instance.actionList[0].ReturnPosition(out int myX, out int myY);
        int targetX = 0;

        if (entitySkill == EntitySkill.PlayerSkill && targetUnit == TargetUnit.PlayerUnits)
        {
            targetX = Mathf.Clamp(myX + horizontalOffset, 0, 4);
            targetGrid = GridManager.Instance.PlayerGrid;
        }
        else if (entitySkill == EntitySkill.PlayerSkill && targetUnit == TargetUnit.EnemyUnits)
        {
            targetX = myX + horizontalOffset;
            targetGrid = GridManager.Instance.PlayerGrid;
            if (targetX > 4)
            {
                targetX = targetX - 5;
                otherGrid = true;
                targetGrid = GridManager.Instance.EnemyGrid;
            }
        }
        else if (entitySkill == EntitySkill.EnemySkill && targetUnit == TargetUnit.EnemyUnits)
        {
            targetX = Mathf.Clamp(myX + horizontalOffset, 0, 4);
            targetGrid = GridManager.Instance.EnemyGrid;
        }
        else if (entitySkill == EntitySkill.EnemySkill && targetUnit == TargetUnit.PlayerUnits)
        {
            targetX = myX + horizontalOffset;
            targetGrid = GridManager.Instance.EnemyGrid;
            if (targetX < 0)
            {
                targetX = targetX + 5;
                otherGrid = true;
                targetGrid = GridManager.Instance.PlayerGrid;
            }
        }

        int targetY = Mathf.Clamp(myY + VerticalOffset, 0, 4);

        int x = targetX;
        int y = targetY;
        TargetGroup newTargetGroup = new TargetGroup();
        newTargetGroup.Tiles = new Tile.TileObject[numberOfTiles];
        //Si la skill es de una unidad aliada dirigida a aliados
        if (entitySkill == EntitySkill.PlayerSkill && targetUnit == TargetUnit.PlayerUnits)
        {
            for (int i = 0; i < newTargetGroup.Tiles.Length; i++)
            {
                if (targetGrid.GetTile(x, y) != null)
                {
                    newTargetGroup.Tiles[i] = targetGrid.GetTile(x, y);
                }
                if (area == Area.Row)
                {
                    x++;
                }
                else
                {
                    y++;
                }
            }
        }
        //Si la skill es de una unidad aliada dirigida a enemigos
        if (entitySkill == EntitySkill.PlayerSkill && targetUnit == TargetUnit.EnemyUnits)
        {
            for (int i = 0; i < newTargetGroup.Tiles.Length; i++)
            {
                if (targetGrid.GetTile(x, y) != null)
                {
                    newTargetGroup.Tiles[i] = targetGrid.GetTile(x, y);
                }
                if (area == Area.Row)
                {
                    x++;
                }
                else
                {
                    y++;
                }

                if (x > 4 && otherGrid == false)
                {
                    x = 0;
                    targetGrid = GridManager.Instance.EnemyGrid;
                    otherGrid = true;
                }
            }
        }
        //Si la skill es de una unidad enemiga dirigida a enemigos
        if (entitySkill == EntitySkill.EnemySkill && targetUnit == TargetUnit.EnemyUnits)
        {
            for (int i = 0; i < newTargetGroup.Tiles.Length; i++)
            {
                if (targetGrid.GetTile(x, y) != null)
                {
                    newTargetGroup.Tiles[i] = targetGrid.GetTile(x, y);
                }
                if (area == Area.Row)
                {
                    x--;
                }
                else
                {
                    y++;
                }
            }
        }
        //Si la skill es de una unidad enemiga dirigida a unidades del jugador
        if (entitySkill == EntitySkill.EnemySkill && targetUnit == TargetUnit.PlayerUnits)
        {
            for (int i = 0; i < newTargetGroup.Tiles.Length; i++)
            {
                if (targetGrid.GetTile(x, y) != null)
                {
                    newTargetGroup.Tiles[i] = targetGrid.GetTile(x, y);
                }
                if (area == Area.Row)
                {
                    x--;
                }
                else
                {
                    y++;
                }

                if (x < 0 && otherGrid == false)
                {
                    x = 4;
                    targetGrid = GridManager.Instance.PlayerGrid;
                    otherGrid = true;
                }
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
