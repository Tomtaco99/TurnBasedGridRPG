using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Grid<TileObject> grid;
    public Tile otherGrid;
    private TileObject[,] tileGrid;
    public TileObject[] allTiles;

    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    public Tile(int width, int height, float cellSize, Vector3 originPosition)
    {
        grid = new Grid<TileObject>(width, height, cellSize, originPosition, (Grid<TileObject> g, int x, int y) => new TileObject(g, x, y), true, true);
        tileGrid = grid.GetAllGridObjects();
        int i = 0;
        allTiles = new TileObject[tileGrid.GetLength(0) * tileGrid.GetLength(1)];
        //Debug.Log(allTiles.Length);
        for (int x = 0; x < tileGrid.GetLength(0); x++)
        {
            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                allTiles[i] = tileGrid[x, y];
                i++;
            }
        }

    }

    public void SetTileType(Vector3 worldPosition, TileObject.TileType tileType)
    {
        TileObject tileObject = grid.GetGridObject(worldPosition);
        if (tileObject != null)
        {
            tileObject.SetTileType(tileType);
        }
    }

    public void SetTileType(int x, int y, TileObject.TileType tileType)
    {
        TileObject tileObject = grid.GetGridObject(x, y);
        if (tileObject != null)
        {
            tileObject.SetTileType(tileType);
        }
    }

    public TileObject GetTile(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    public TileObject GetTile(Vector3 worldPosition)
    {
        return grid.GetGridObject(worldPosition);
    }

    public void MoveEntity(Vector3 worldPosition, Movement ent)
    {
        TileObject tileObject = grid.GetGridObject(worldPosition);
        if (tileObject != null)
        {
            tileObject.MoveEntity(ent);
        }
    }

    public void MoveEntity(int x, int y, Movement ent)
    {
        TileObject tileObject = grid.GetGridObject(x, y);
        if (tileObject != null)
        {
            tileObject.MoveEntity(ent);
        }
    }

    public Movement[] GetAllEntities()
    {
        Movement[] MovementEntities = new Movement[25];
        for (int i = 0; i < allTiles.Length; i++)
        {
            MovementEntities[i] = allTiles[i].GetEntity();
        }
        return MovementEntities;
    }

    public void MeleeDamage(Vector3 worldPosition, int damage, Direction attackDirection, Entity.EntityType entityTypeToDamage)
    {
        TileObject originalTile = grid.GetGridObject(worldPosition);
        TileObject[] tilesToDamage = new TileObject[2];

        if (originalTile != null)
        {
            int x;
            int y;
            int enemyX = 0;
            int playerNewX = 4;
            originalTile.ReturnTilePos(out x, out y);
            switch (attackDirection)
            {
                case Direction.Right:
                    x++;
                    for (int i = 0; i < 2; i++)
                    {
                        if (x > 4)
                        {
                            tilesToDamage[i] = otherGrid.grid.GetGridObject(enemyX, y);
                            enemyX++;
                        }
                        else
                        {
                            tilesToDamage[i] = grid.GetGridObject(x, y);
                            x++;
                        }
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].entity.entityType != originalTile.entity.entityType)
                            {
                                if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                    break;
                            }
                            else
                            {

                            }

                        }
                    }
                    break;
                case Direction.Left:
                    x--;
                    for (int i = 0; i < 2; i++)
                    {
                        if (x < 0)
                        {
                            tilesToDamage[i] = otherGrid.grid.GetGridObject(playerNewX, y);
                            playerNewX--;
                        }
                        else
                        {
                            tilesToDamage[i] = grid.GetGridObject(x, y);
                            x--;
                        }
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].entity.entityType != originalTile.entity.entityType)
                            {
                                if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                    break;
                            }
                            else
                            {

                            }
                        }
                    }
                    break;
                case Direction.Up:
                    for (int i = 0; i < 2; i++)
                    {
                        tilesToDamage[i] = grid.GetGridObject(x, y);
                        y++;
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].entity.entityType != originalTile.entity.entityType)
                            {
                                if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                    break;
                            }
                            else
                            {

                            }
                        }
                    }
                    break;
                case Direction.Down:
                    for (int i = 0; i < 2; i++)
                    {
                        tilesToDamage[i] = grid.GetGridObject(x, y);
                        y--;
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].entity.entityType != originalTile.entity.entityType)
                            {
                                if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                    break;
                            }
                            else
                            {

                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public TileObject[] ObjectiveTiles(int xGrid, int yGrid, int range, Direction attackDirection)
    {
        TileObject originalTile = grid.GetGridObject(xGrid, yGrid);
        TileObject[] tilesToDamage = new TileObject[range];
        if (originalTile != null)
        {
            int x;
            int y;
            int enemyX = 0;
            int playerNewX = 4;
            originalTile.ReturnTilePos(out x, out y);
            switch (attackDirection)
            {
                case Direction.Right:
                    x++;
                    for (int i = 0; i < tilesToDamage.Length; i++)
                    {
                        if (x > 4)
                        {
                            tilesToDamage[i] = otherGrid.grid.GetGridObject(enemyX, y);
                            enemyX++;
                        }
                        else
                        {
                            tilesToDamage[i] = grid.GetGridObject(x, y);
                            x++;
                        }
                    }

                    break;
                case Direction.Left:
                    x--;
                    for (int i = 0; i < tilesToDamage.Length; i++)
                    {
                        if (x < 0)
                        {
                            tilesToDamage[i] = otherGrid.grid.GetGridObject(playerNewX, y);
                            playerNewX--;
                        }
                        else
                        {
                            tilesToDamage[i] = grid.GetGridObject(x, y);
                            x--;
                        }
                    }
                    break;
                case Direction.Up:
                    y++;
                    for (int i = 0; i < tilesToDamage.Length; i++)
                    {
                        tilesToDamage[i] = grid.GetGridObject(x, y);
                        y++;
                    }
                    break;
                case Direction.Down:
                    y--;
                    for (int i = 0; i < tilesToDamage.Length; i++)
                    {
                        tilesToDamage[i] = grid.GetGridObject(x, y);
                        y--;
                    }
                    break;

                default:
                    break;
            }


        }
        return tilesToDamage;
    }

    public void MeleeDamage(int xGrid, int yGrid, int damage, Direction attackDirection, Entity.EntityType entityTypeToDamage)
    {
        TileObject originalTile = grid.GetGridObject(xGrid, yGrid);
        TileObject[] tilesToDamage = new TileObject[2];

        if (originalTile != null)
        {
            int x;
            int y;
            int enemyX = 0;
            int playerNewX = 4;
            originalTile.ReturnTilePos(out x, out y);
            switch (attackDirection)
            {
                case Direction.Right:
                    x++;
                    for (int i = 0; i < 2; i++)
                    {
                        if (x > 4)
                        {
                            tilesToDamage[i] = otherGrid.grid.GetGridObject(enemyX, y);
                            enemyX++;
                        }
                        else
                        {
                            tilesToDamage[i] = grid.GetGridObject(x, y);
                            x++;
                        }
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                break;
                        }
                    }
                    break;
                case Direction.Left:
                    x--;
                    for (int i = 0; i < 2; i++)
                    {
                        if (x < 0)
                        {

                            tilesToDamage[i] = otherGrid.grid.GetGridObject(playerNewX, y);
                            playerNewX--;
                        }
                        else
                        {
                            tilesToDamage[i] = grid.GetGridObject(x, y);
                            x--;
                        }
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                break;
                        }
                    }
                    break;
                case Direction.Up:
                    y++;
                    for (int i = 0; i < 2; i++)
                    {
                        tilesToDamage[i] = grid.GetGridObject(x, y);
                        y++;
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                break;
                        }
                    }
                    break;
                case Direction.Down:
                    y--;
                    for (int i = 0; i < 2; i++)
                    {
                        tilesToDamage[i] = grid.GetGridObject(x, y);
                        y--;
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                break;
                        }
                    }
                    break;

                default:
                    break;
            }

        }
    }

    public void RangedDamage(Vector3 worldPosition, int damage, int range, Direction attackDirection, Entity.EntityType entityTypeToDamage)
    {
        TileObject originalTile = grid.GetGridObject(worldPosition);
        TileObject[] tilesToDamage = new TileObject[range];

        if (originalTile != null)
        {
            int x;
            int y;
            int enemyX = 0;
            int playerNewX = 4;
            originalTile.ReturnTilePos(out x, out y);
            switch (attackDirection)
            {
                case Direction.Right:
                    x++;
                    for (int i = 0; i < range; i++)
                    {
                        if (x > 4)
                        {
                            tilesToDamage[i] = otherGrid.grid.GetGridObject(enemyX, y);
                            enemyX++;
                        }
                        else
                        {
                            tilesToDamage[i] = grid.GetGridObject(x, y);
                            x++;
                        }

                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].entity.entityType != originalTile.entity.entityType)
                                if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                    break;
                        }
                        damage--;
                    }
                    break;
                case Direction.Left:
                    x--;
                    for (int i = 0; i < range; i++)
                    {
                        if (x < 0)
                        {
                            tilesToDamage[i] = otherGrid.grid.GetGridObject(playerNewX, y);
                            playerNewX--;
                        }
                        else
                        {
                            tilesToDamage[i] = grid.GetGridObject(x, y);
                            x--;
                        }
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].entity.entityType != originalTile.entity.entityType)
                                if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                    break;
                        }
                        damage--;
                    }
                    break;
                case Direction.Up:
                    y++;
                    for (int i = 0; i < range; i++)
                    {
                        tilesToDamage[i] = grid.GetGridObject(x, y);
                        y++;
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].entity.entityType != originalTile.entity.entityType)
                                if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                    break;
                        }
                        damage--;
                    }
                    break;
                case Direction.Down:
                    y--;
                    for (int i = 0; i < range; i++)
                    {
                        tilesToDamage[i] = grid.GetGridObject(x, y);
                        y--;
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].entity.entityType != originalTile.entity.entityType)
                                if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                    break;
                        }
                        damage--;
                    }
                    break;

                default:
                    break;
            }

        }
    }

    public void RangedDamage(int xGrid, int yGrid, int damage, int range, Direction attackDirection, Entity.EntityType entityTypeToDamage)
    {
        TileObject originalTile = grid.GetGridObject(xGrid, yGrid);
        TileObject[] tilesToDamage = new TileObject[range];

        if (originalTile != null)
        {
            int x;
            int y;
            int enemyX = 0;
            int playerNewX = 4;
            originalTile.ReturnTilePos(out x, out y);
            switch (attackDirection)
            {
                case Direction.Right:
                    x++;
                    for (int i = 0; i < range; i++)
                    {
                        if (x > 4)
                        {
                            tilesToDamage[i] = otherGrid.grid.GetGridObject(enemyX, y);
                            enemyX++;
                        }
                        else
                        {
                            tilesToDamage[i] = grid.GetGridObject(x, y);
                            x++;
                        }

                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                break;
                        }
                        damage--;
                    }
                    break;
                case Direction.Left:
                    x--;
                    for (int i = 0; i < range; i++)
                    {
                        if (x < 0)
                        {
                            tilesToDamage[i] = otherGrid.grid.GetGridObject(playerNewX, y);
                            playerNewX--;
                        }
                        else
                        {
                            tilesToDamage[i] = grid.GetGridObject(x, y);
                            x--;
                        }
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                break;
                        }
                        damage--;
                    }
                    break;
                case Direction.Up:
                    for (int i = 0; i < range; i++)
                    {
                        tilesToDamage[i] = grid.GetGridObject(x, y);
                        y++;
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                break;
                        }
                        damage--;
                    }
                    break;
                case Direction.Down:
                    for (int i = 0; i < range; i++)
                    {
                        tilesToDamage[i] = grid.GetGridObject(x, y);
                        y--;
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].TryDamageEntity(damage, entityTypeToDamage))
                                break;
                        }
                        damage--;
                    }
                    break;

                default:
                    break;
            }

        }
    }

    [System.Serializable]
    public class TileObject
    {
        public enum TileType
        {
            Standard,
            Blocked,
            OnFire,
        }
        private Grid<TileObject> grid;
        private int x;
        private int y;
        private TileType tileType;
        public Movement EntityMovement;
        public Entity entity;
        public GameObject tileSprite;
        private string entityName = "noEntity";
        public bool isOcupied => EntityMovement != null ? true : false;
        public bool isBlocked => tileType == TileType.Blocked ? true : false;
        private Color untargetedColor = new Color(0.6f, 0.6f, 0.6f);
        private Color targetedColor = new Color(1, 0, 0, 0.5f);
        private Color inRangeColor = new Color(0, 0, 1, 0.5f);
        public TileObject(Grid<TileObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetTileType(TileType tileType)
        {
            this.tileType = tileType;
            grid.TriggerGridObjectChanged(x, y);
        }

        public void NullifyEntity()
        {
            EntityMovement = null;
            entity = null;
        }

        public void UpdateEntity()
        {
            if (EntityMovement != null)
            {
                entityName = EntityMovement.gameObject.name;
            }
            else
            {
                entityName = "noEntity";
            }
            grid.TriggerGridObjectChanged(x, y);
        }

        public void DeselectTile()
        {
            NullifyEntity();
            UpdateEntity();
        }

        public override string ToString()
        {
            return x + "," + y + "\n" + tileType.ToString() + "\n" + entityName;
        }

        public void MoveEntity(Movement ent)
        {
            entityName = ent.gameObject.name;
            float cellSize = grid.GetCellSize();
            EntityMovement = ent;
            EntityMovement.gameObject.transform.position = grid.GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f;
            grid.TriggerGridObjectChanged(x, y);
            entity = EntityMovement.gameObject.GetComponent<Entity>();
        }

        public void ReturnTilePos(out int xpos, out int ypos)
        {
            xpos = x;
            ypos = y;
        }

        public bool TryDamageEntity(int damage, Entity.EntityType entityTypeToDamage)
        {

            if (EntityMovement != null)
            {
                if (entity.entityType == entityTypeToDamage)
                {
                    entity.RecieveDamage(damage);
                    Debug.Log(damage + " done to " + entityName);
                    return true;
                }
                else { return false; }

            }
            else
            {
                Debug.Log("TilePos: " + x + "," + y + " no entity to damage (tried to do " + damage + ")");
                return false;
            }

            //return false;
        }

        public Movement GetEntity()
        {
            if (EntityMovement != null)
            {
                return EntityMovement;
            }
            else
            {
                return null;
            }
        }

        public Vector3 GetWorldPosition()
        {
            return grid.GetTextWorldPosition(x, y);
        }

        public void TargetTile()
        {
            tileSprite.GetComponent<SpriteRenderer>().sprite = GridManager.Instance.attackSprite.GetComponent<SpriteRenderer>().sprite;
        }
        public void UnTargetTile()
        {
            tileSprite.GetComponent<SpriteRenderer>().sprite = GridManager.Instance.sprite.GetComponent<SpriteRenderer>().sprite;
        }
        public void TileInRange()
        {
            tileSprite.GetComponent<SpriteRenderer>().sprite = GridManager.Instance.moveSprite.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
