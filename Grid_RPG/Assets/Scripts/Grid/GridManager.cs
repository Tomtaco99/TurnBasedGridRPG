using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PM.Utils;
using Sirenix.OdinInspector;

public class GridManager : TemporalSingleton<GridManager>
{
    public Tile PlayerGrid;
    public Tile EnemyGrid;

    public GameObject sprite;
    public GameObject moveSprite;
    public GameObject attackSprite;

    public Movement[] PlayerUnits;
    public Movement[] EnemyUnits;
    private int index = 0;



    public override void Awake()
    {
        base.Awake();
        PlayerGrid = new Tile(5, 5, 4, Vector3.zero);
        EnemyGrid = new Tile(5, 5, 4, new Vector3(25, 0));
        PlayerGrid.otherGrid = EnemyGrid;
        EnemyGrid.otherGrid = PlayerGrid;


        //ONLY FOR TESTING...........................
        foreach (Movement item in PlayerUnits)
        {
            SetNewEntityPosition(index, index, item, PlayerGrid);
            index++;
        }
        SetNewEntityPosition(1, 0, PlayerUnits[0], PlayerGrid);
        SetNewEntityPosition(2, 3, PlayerUnits[1], PlayerGrid);
        SetNewEntityPosition(1, 2, PlayerUnits[2], PlayerGrid);
        SetNewEntityPosition(4, 3, PlayerUnits[3], PlayerGrid);

        SetNewEntityPosition(0, 2, EnemyUnits[0], EnemyGrid);
        SetNewEntityPosition(2, 3, EnemyUnits[1], EnemyGrid);
        SetNewEntityPosition(3, 1, EnemyUnits[2], EnemyGrid);

        for (int i = 0; i < PlayerUnits.Length; i++)
        {
            PlayerUnits[i].myGrid = PlayerGrid;
        }
        for (int i = 0; i < EnemyUnits.Length; i++)
        {
            EnemyUnits[i].myGrid = EnemyGrid;
        }

        for (int i = 0; i < PlayerGrid.allTiles.Length; i++)
        {
            PlayerGrid.allTiles[i].tileSprite = Instantiate(sprite, PlayerGrid.allTiles[i].GetWorldPosition(), Quaternion.identity);
        }
        for (int i = 0; i < EnemyGrid.allTiles.Length; i++)
        {
            EnemyGrid.allTiles[i].tileSprite = Instantiate(sprite, EnemyGrid.allTiles[i].GetWorldPosition(), Quaternion.identity);
        }

        //............................................
    }
    //ONLY FOR TESTING................................................................
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 mouseWorldPosition = Utilities.GetMouseWorldPosition();
            PlayerGrid.SetTileType(mouseWorldPosition, Tile.TileObject.TileType.Standard);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector3 mouseWorldPosition = Utilities.GetMouseWorldPosition();
            PlayerGrid.SetTileType(mouseWorldPosition, Tile.TileObject.TileType.Blocked);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Vector3 mouseWorldPosition = Utilities.GetMouseWorldPosition();
            PlayerGrid.SetTileType(mouseWorldPosition, Tile.TileObject.TileType.OnFire);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 mouseWorldPosition = Utilities.GetMouseWorldPosition();
            //PlayerGrid.RangedDamage(mouseWorldPosition, 5, 5, Tile.Direction.Right);
            //PlayerF.MeleeDamage(mouseWorldPosition, 5, Tile.Direction.Right);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Movement[] units = EnemyGrid.GetAllEntities();
            for (int i = 0; i < units.Length; i++)
            {
                if (units[i] != null)
                    Debug.Log(units[i].name);
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Vector3 mouseWorldPosition = Utilities.GetMouseWorldPosition();
            //GridAttacks.Instance.RowAttack(mouseWorldPosition, 2, EnemyGrid);
            //PlayerF.MeleeDamage(mouseWorldPosition, 5, Tile.Direction.Right);
        }
    }
    //..........................................................................................
    public Tile GetPlayerGrid
    {
        get { return PlayerGrid; }
    }

    public Tile GetEnemyGrid
    {
        get { return EnemyGrid; }
    }

    public void SetNewEntityPosition(int x, int y, Movement ent, Tile grid)
    {
        grid.MoveEntity(x, y, ent);
        ent.SetMyTile(grid.GetTile(x, y));
    }

    public void RemoveCharacter(Movement character)
    {
        for (int i = 0; i < PlayerUnits.Length; i++)
        {
            if (PlayerUnits[i] == character)
            {
                PlayerUnits[i].DeselectTile();
                PlayerUnits[i] = null;
                return;
            }
        }
        for (int i = 0; i < EnemyUnits.Length; i++)
        {
            if (EnemyUnits[i] == character)
            {
                EnemyUnits[i].DeselectTile();
                EnemyUnits[i] = null;
                return;
            }
        }
    }
}
