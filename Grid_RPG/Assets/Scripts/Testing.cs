using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PM.Utils;

public class Testing : MonoBehaviour
{
    [SerializeField] private HeatMapVisual heatMapVisual;
    [SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
    [SerializeField] private HeatMapGenericVisual heatMapGenericVisual;
    private Grid<HeatMapGridObject> grid;

    // Start is called before the first frame update
    void Start()
    {
        //grid = new Grid<HeatMapGridObject>(3, 3, 6, new Vector3(-10, 0), (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
        //heatMapVisual.SetGrid(grid);
        //heatMapBoolVisual.SetGrid(grid);
        heatMapGenericVisual.SetGrid(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Utilities.GetMouseWorldPosition();
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            if (heatMapGridObject != null)
            {
                heatMapGridObject.AddValue(5);
            }
        }
    }
}

public class HeatMapGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;

    private Grid<HeatMapGridObject> grid;
    private int x;
    private int y;
    private int value;

    public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
