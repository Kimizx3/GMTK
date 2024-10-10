using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField]
    Transform testTransform;
    private Grid<Building> grid;
    private void Awake() {
        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10f;
        grid = new Grid<Building>(gridWidth, gridHeight, cellSize, Vector3.zero, (Grid<Building> g, int x, int y) => new Building(g, x, y));
    }


    public class Building
    {
        private Grid<Building> grid;
        private int x;
        private int y;
        public Building(Grid<Building> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.GetXY(UtilsClass.GetMouseWorldPosition(), out int x, out int y);
            
            Instantiate(testTransform, grid.GetCellCenterWorldPosition(x, y), Quaternion.identity);
        }
        
    }
}
