using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GridSetup : SerializedMonoBehaviour
{
    [HorizontalGroup("Grids")]
    [TableMatrix(SquareCells = true, HorizontalTitle = "PlayerGrid", IsReadOnly = true)]
    public Movement[,] PlayerGrid;
    [HorizontalGroup("Grids")]
    [TableMatrix(SquareCells = true, HorizontalTitle = "EnemyGrid", IsReadOnly = true)]
    public Movement[,] EnemyGrid;

    private GridManager gridManager;

    private void Awake()
    {
        gridManager = GetComponent<GridManager>();
    }
    private void Start()
    {
        SetUpEntities();
    }

#if UNITY_EDITOR // Editor-related code must be excluded from builds
    [OnInspectorInit]
    private void SetUpGrid()
    {
        PlayerGrid = new Movement[5, 5]{
            { null, null, null, null, GridManager.Instance.PlayerUnits[0] },
            { null, null, null, GridManager.Instance.PlayerUnits[1],null },
            { null, null, GridManager.Instance.PlayerUnits[2], null,null },
            { null, GridManager.Instance.PlayerUnits[3], null, null,null },
            { null, null, null, null,null },
        };
        EnemyGrid = new Movement[5, 5]{
            { null, null, GridManager.Instance.EnemyUnits[0], null, null },
            { GridManager.Instance.EnemyUnits[1], null, null, null, null },
            { null, null, null, null,                               null },
            { null, null, null, GridManager.Instance.EnemyUnits[2], null },
            { null, null, null, null,                               null },
        };
    }
#endif
    private void SetUpEntities()
    {
        for (int x = 0; x < PlayerGrid.GetLength(0); x++)
        {
            for (int y = 0; y < PlayerGrid.GetLength(1); y++)
            {
                if (PlayerGrid[x, y] != null)
                {
                    ConvertGridPosition(x, y, out int xGrid, out int yGrid);
                    gridManager.SetNewEntityPosition(x, yGrid, PlayerGrid[x, y], gridManager.PlayerGrid);
                    //Debug.Log(PlayerGrid[x, y].name);
                }
            }
        }
        for (int x = 0; x < EnemyGrid.GetLength(0); x++)
        {
            for (int y = 0; y < EnemyGrid.GetLength(1); y++)
            {
                if (EnemyGrid[x, y] != null)
                {
                    ConvertGridPosition(x, y, out int xGrid, out int yGrid);
                    gridManager.SetNewEntityPosition(x, yGrid, EnemyGrid[x, y], gridManager.EnemyGrid);
                    //Debug.Log(EnemyGrid[x, y].name);
                }
            }
        }
    }

    private void ConvertGridPosition(int x, int y, out int xGrid, out int yGrid)
    {

        switch (x)
        {
            case 0:
                xGrid = 4;
                break;
            case 1:
                xGrid = 3;
                break;
            case 2:
                xGrid = 2;
                break;
            case 3:
                xGrid = 1;
                break;
            case 4:
                xGrid = 0;
                break;
            default:
                xGrid = 0;
                break;
        }
        switch (y)
        {
            case 0:
                yGrid = 4;
                break;
            case 1:
                yGrid = 3;
                break;
            case 2:
                yGrid = 2;
                break;
            case 3:
                yGrid = 1;
                break;
            case 4:
                yGrid = 0;
                break;
            default:
                yGrid = 0;
                break;
        }

    }
}
