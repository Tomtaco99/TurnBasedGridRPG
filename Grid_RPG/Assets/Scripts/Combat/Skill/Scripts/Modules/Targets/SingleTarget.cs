using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Skills/Targets/Single Target")]
public class SingleTarget : Target
{
    public bool targetSelf;
    public enum EntitySkill
    {
        PlayerSkill,
        EnemySkill
    }

    [HideIf("targetSelf")]
    public EntitySkill entitySkill;

    public override TargetGroup GetTargets()
    {
        throw new System.NotImplementedException();
    }

    public override TargetGroup GetTargets(Movement character)
    {
        throw new System.NotImplementedException();
    }

    public override TargetGroup GetTargets(Tile position)
    {
        throw new System.NotImplementedException();
    }

    public override TargetGroup GetTargets(Vector3 worldPosition)
    {
        TargetGroup newTargetGroup = new TargetGroup();
        if (!targetSelf)
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
            if (targetTile == null)
            {
                return newTargetGroup;
            }
            targetTile.ReturnTilePos(out int x, out int y);
            newTargetGroup.Tiles[0] = targetTile;
            return newTargetGroup;
        }
        else
        {
            Tile.TileObject targetTile = CombatManager.Instance.actionList[0].GetMyTile;
            newTargetGroup.Tiles[0] = targetTile;
            return newTargetGroup;
        }

    }
}
