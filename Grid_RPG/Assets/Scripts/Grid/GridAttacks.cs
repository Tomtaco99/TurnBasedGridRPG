using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAttacks : TemporalSingleton<GridAttacks>
{
    /*
    public void VerticalMeleeAttack(int xGrid, int yGrid, int damage, int offset, int amountOfTilesToDamage, Tile.Direction attackDirection, Tile myGrid, Tile targetGrid)
    {
        Tile.TileObject originalTile = myGrid.GetTile(xGrid, yGrid);

        Tile.TileObject[] tilesToDamage = new Tile.TileObject[amountOfTilesToDamage];

        if (originalTile != null)
        {
            int x;
            int y;
            int enemyX = 0;
            int playerNewX = 4;
            originalTile.ReturnTilePos(out x, out y);
            switch (attackDirection)
            {
                case Tile.Direction.Right:
                    x += offset;
                    if (x > 4)
                    {
                        enemyX += offset;
                    }
                    y--;
                    for (int i = 0; i < tilesToDamage.Length; i++)
                    {
                        if (x > 4)
                        {

                            tilesToDamage[i] = targetGrid.grid.GetGridObject(enemyX, y);
                            y++;
                        }
                        else
                        {
                            tilesToDamage[i] = myGrid.GetTile(x, y);
                            y++;
                        }
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            tilesToDamage[i].TryDamageEntity(damage);
                        }
                    }
                    break;
                case Tile.Direction.Left:
                    x -= offset;
                    if (x < 0)
                    {
                        playerNewX -= offset;
                    }
                    y--;
                    for (int i = 0; i < tilesToDamage.Length; i++)
                    {
                        if (x < 0)
                        {
                            tilesToDamage[i] = targetGrid.grid.GetGridObject(playerNewX, y);
                            y++;
                        }
                        else
                        {
                            tilesToDamage[i] = myGrid.GetTile(x, y);
                            y++;
                        }
                        if (damage < 0)
                            damage = 0;
                        if (tilesToDamage[i] != null)
                        {
                            if (tilesToDamage[i].TryDamageEntity(damage))
                                break;
                        }
                    }
                    break;

                default:
                    break;
            }

        }
    }
    public void ColumnAttack(int xGrid, int damage, Tile targetGrid)
    {
        Tile.TileObject[] tilesToDamage = new Tile.TileObject[5];

        for (int i = 0; i < tilesToDamage.Length; i++)
        {
            tilesToDamage[i] = targetGrid.GetTile(xGrid, i);

            if (tilesToDamage[i] != null)
            {
                tilesToDamage[i].TryDamageEntity(damage);
            }
        }
    }

    public void ColumnAttack(Vector3 worldPosition, int damage, Tile targetGrid)
    {
        Tile.TileObject targetTile = targetGrid.grid.GetGridObject(worldPosition);
        targetTile.ReturnTilePos(out int x, out int y);
        Tile.TileObject[] tilesToDamage = new Tile.TileObject[5];

        for (int i = 0; i < tilesToDamage.Length; i++)
        {
            tilesToDamage[i] = targetGrid.GetTile(x, i);

            if (tilesToDamage[i] != null)
            {
                tilesToDamage[i].TryDamageEntity(damage);
            }
        }
    }

    public void RowAttack(int yGrid, int damage, Tile targetGrid)
    {
        Tile.TileObject[] tilesToDamage = new Tile.TileObject[5];

        for (int i = 0; i < tilesToDamage.Length; i++)
        {
            tilesToDamage[i] = targetGrid.GetTile(i, yGrid);

            if (tilesToDamage[i] != null)
            {
                tilesToDamage[i].TryDamageEntity(damage);
            }
        }
    }

    public void RowAttack(Vector3 worldPosition, int damage, Tile targetGrid)
    {
        Tile.TileObject targetTile = targetGrid.grid.GetGridObject(worldPosition);
        targetTile.ReturnTilePos(out int x, out int y);
        Tile.TileObject[] tilesToDamage = new Tile.TileObject[5];

        for (int i = 0; i < tilesToDamage.Length; i++)
        {
            tilesToDamage[i] = targetGrid.GetTile(i, y);

            if (tilesToDamage[i] != null)
            {
                tilesToDamage[i].TryDamageEntity(damage);
            }
        }
    }
*/
}
