using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public Grid<PathNode> grid;
    public int x;
    public int y;
    public int gCost;
    public int hCost;
    public int fCost;
    public PathNode cameFromNode;
    private PlacedObject transform;
    public bool isWalkable = true;
    
   public PathNode(Grid<PathNode> grid, int x, int y)
   {
       this.grid = grid;
       this.x = x;
       this.y = y;
   }

   public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public override string ToString()
    {
         return x + ", " + y;
    }

    public void SetTransform(PlacedObject transform)
    {
        this.transform = transform;
        grid.TriggerGridObjectChanged(x, y);
    }
    public void ClearTransform()
    {
        transform = null;
        grid.TriggerGridObjectChanged(x, y);
    }
    public PlacedObject GetPlacedObject()
    {
        return transform;
    }

    public bool CanBuild()
    {
        return transform == null;
    }
}
