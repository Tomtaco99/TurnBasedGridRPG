using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;


[CreateAssetMenu(menuName = "Grid/NewGridPositioningObject")]
public class GridPosition : SerializedScriptableObject, ISerializationCallbackReceiver
{
    [HorizontalGroup("Grids")]
    [TableMatrix(SquareCells = true, HorizontalTitle = "PlayerGrid")]
    [SerializeField]
    public Movement[,] PlayerGrid;
    [HorizontalGroup("Grids")]
    [TableMatrix(SquareCells = true, HorizontalTitle = "EnemyGrid", IsReadOnly = true)]
    public Movement[,] EnemyGrid;

    private static Movement[,] saveGrid;
    private static bool isSaved = false;

#if UNITY_EDITOR // Editor-related code must be excluded from builds
    [OnInspectorInit]
    private void SetUpGrid()
    {
        if (!isSaved)
        {
            PlayerGrid = new Movement[5, 5];
        }
        else
        {
            PlayerGrid = saveGrid;
        }
        EnemyGrid = new Movement[5, 5];
    }


    [OnInspectorDispose]
    public void SaveGrids()
    {
        isSaved = true;
        for (int x = 0; x < PlayerGrid.GetLength(0); x++)
        {
            for (int y = 0; y < PlayerGrid.GetLength(1); y++)
            {
                if (PlayerGrid != null)
                {
                    saveGrid = PlayerGrid;
                }
            }
        }
    }
#endif
}
