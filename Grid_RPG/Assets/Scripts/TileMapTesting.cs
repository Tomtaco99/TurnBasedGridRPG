using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PM.Utils;

public class TileMapTesting : MonoBehaviour
{
    public Tile PlayerGrid;
    public Tile enemyGrid;
    public Movement[] allEntities;
    private int index = 0;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerGrid = new Tile(5, 5, 4, Vector3.zero);
        enemyGrid = new Tile(5, 5, 4, new Vector3(25, 0));
        PlayerGrid.otherGrid = enemyGrid;
        enemyGrid.otherGrid = PlayerGrid;
        foreach (Movement item in allEntities)
        {
            SetNewEntityPosition(index, index, item);
            index++;
        }
        //SetNewEntityPosition(2, 2);
        //EntityTest.OnSelect+=
    }

    private void Update()
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

    }

    public Tile GetTile
    {
        get { return PlayerGrid; }
    }

    private void SetNewEntityPosition(int x, int y, Movement ent)
    {
        PlayerGrid.MoveEntity(x, y, ent);
        ent.SetMyTile(PlayerGrid.GetTile(x, y));
    }
}
