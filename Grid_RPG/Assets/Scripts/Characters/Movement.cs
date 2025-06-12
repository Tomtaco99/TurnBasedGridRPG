using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PM.Utils;

public class Movement : MonoBehaviour
{
    private GridManager testing;
    public Tile myGrid;
    private Tile.TileObject myTile;
    private Tile.TileObject oldTile;
    private Tile.TileObject[] allTiles;
    private Tile.TileObject[] inRangeTiles;
    private bool isSelected;
    [Range(0, 4)]
    public int range = 2;
    public float GeneralPrio;
    public Entity entity;
    public CombatManager CM;
    public bool IsSelected { get { return isSelected; } set { isSelected = value; DeSelect(); } }
    public Tile.TileObject GetMyTile { get { return myTile; } }

    private void Start()
    {
        testing = GridManager.Instance;
        entity = GetComponent<Entity>();
        CM = CombatManager.Instance;
        entity.self = gameObject.GetComponent<SpriteRenderer>().sprite;
        allTiles = myGrid.allTiles;
    }

    private void Awake()
    {
        //entity = GetComponent<Entity>();
        //entity.self = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && IsSelected)
        {
            Vector3 mouseWorldPosition = Utilities.GetMouseWorldPosition();
            //tileMap.SetTileMapSprite(mouseWorldPosition, TileMap.TileMapObject.TileMapSprite.Ground);
            SetNewEntityPosition(mouseWorldPosition);

        }

    }

    private void SetNewEntityPosition(Vector3 mouseWorldPosition)
    {
        if (myGrid == null)
        {
            myGrid = testing.GetPlayerGrid;
        }
        if (myGrid.GetTile(mouseWorldPosition) != null)
        {

            if (!IsTileOccupied(myGrid.GetTile(mouseWorldPosition)))
            {
                Tile.TileObject clickedTile = myGrid.GetTile(mouseWorldPosition);

                clickedTile.ReturnTilePos(out int newX, out int newY);
                myTile.ReturnTilePos(out int currentX, out int currentY);
                bool inRange = Mathf.Abs(newX - currentX) <= range && Mathf.Abs(newY - currentY) <= range;
                if (inRange)
                {
                    this.SetMyTile(myGrid.GetTile(mouseWorldPosition));
                    myGrid.MoveEntity(mouseWorldPosition, this);
                    DeSelect();
                    CM.MoveButton.interactable = false;
                    CM.GuardButton.interactable = true;
                    CM.AttackButton.interactable = true;
                    CM.SkillButton.interactable = true;
                    CM.Skill1.interactable = false;
                    CM.Skill2.interactable = false;
                    CM.buttonSelected = false;
                    CM.hasMoved = true;
                    for (int i = 0; i < allTiles.Length; i++)
                    {
                        allTiles[i].UnTargetTile();
                    }
                }
                else
                {
                    Debug.Log("I cant go there");
                }

            }
        }

    }

    public void SetNewEntityPosition(int x, int y)
    {
        if (myGrid == null)
        {
            myGrid = testing.GetPlayerGrid;
        }
        if (myGrid.GetTile(x, y) != null)
        {
            if (!IsTileOccupied(myGrid.GetTile(x, y)))
            {
                Tile.TileObject clickedTile = myGrid.GetTile(x, y);

                clickedTile.ReturnTilePos(out int newX, out int newY);
                myTile.ReturnTilePos(out int currentX, out int currentY);

                this.SetMyTile(myGrid.GetTile(x, y));
                myGrid.MoveEntity(x, y, this);
                UpdatePrio();
            }
        }


    }

    private bool IsTileOccupied(Tile.TileObject tile)
    {
        if (tile.isOcupied || tile.isBlocked)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetMyTile(Tile.TileObject tile)
    {
        if (tile != myTile)
        {

            oldTile = myTile;
            myTile = tile;
            myTile.ReturnTilePos(out int x, out int y);
            //Debug.Log(this.name + "'s grid position is: " + x + " " + y);
            if (oldTile != null)
                oldTile.DeselectTile();
        }
    }


    public void SelectEntity()
    {
        isSelected = true;
        GetComponent<SpriteRenderer>().color = Color.red;

        EntityController.Instance.TryGetSelected(this);
    }

    public void DeSelect()
    {
        isSelected = false;
        GetComponent<SpriteRenderer>().color = Color.white;

    }

    public void ReturnPosition(out int x, out int y)
    {
        myTile.ReturnTilePos(out int xGrid, out int yGrid);
        x = xGrid;
        y = yGrid;
    }

    public void UpdatePrio()
    {
        myTile.ReturnTilePos(out int xGrid, out int yGrid);
        GeneralPrio = xGrid + (float)yGrid / 10;
    }

    public void UpdatePrioRight()
    {
        myTile.ReturnTilePos(out int xGrid, out int yGrid);
        GeneralPrio = 4 - xGrid + (float)yGrid / 10;
    }

    public void DeselectTile()
    {
        myTile.DeselectTile();
    }

    public void MarkTilesInRange()
    {



        myTile.ReturnTilePos(out int currentX, out int currentY);
        for (int i = 0; i < allTiles.Length; i++)
        {
            allTiles[i].ReturnTilePos(out int newX, out int newY);
            bool inRange = Mathf.Abs(newX - currentX) <= range && Mathf.Abs(newY - currentY) <= range;

            if (inRange)
            {
                allTiles[i].TileInRange();
            }

            if (IsTileOccupied(allTiles[i]))
            {
                allTiles[i].UnTargetTile();
            }

        }
        myTile.UnTargetTile();
    }
}
