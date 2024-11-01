using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridManager : MonoBehaviour
{
    private PathFinding pathFinding;
    public GetListVector3EventChannel GetPathEvent;
    public GridBuildingSystemVariable gridBuildingSystemVariable;
    private Grid<PathNode> grid;
    [SerializeField]
    private int width = 10;
    [SerializeField]
    private int height = 10;
    [SerializeField]
    private float girdSize = 1f;
    private void OnEnable() {
        GetPathEvent.OnEventRaised += GetPath;
    }
    private void OnDisable() {
        GetPathEvent.OnEventRaised -= GetPath;
    }
    private void Start() {
        grid = new Grid<PathNode>(width, height, girdSize, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
        pathFinding = new PathFinding(grid);
        gridBuildingSystemVariable.gridBuildingSystem.grid = grid;
    }

    private void Update() {
        // if (Input.GetMouseButtonDown(0)) {
        //     Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        //     pathFinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
        //     List<PathNode> path = pathFinding.FindPath(0, 0, x, y);
        //     if(path != null)
        //     {
        //         for (int i = 0; i < path.Count - 1; i++)
        //         {
        //             Debug.DrawLine(new Vector3(path[i].x, path[i].y) * pathFinding.GetGrid().GetCellSize() + Vector3.one * pathFinding.GetGrid().GetCellSize() * .5f, new Vector3(path[i + 1].x, path[i + 1].y) * pathFinding.GetGrid().GetCellSize() + Vector3.one * pathFinding.GetGrid().GetCellSize() * .5f, Color.green, 1f);
        //         }
        //     }
        //     Debug.Log(x + ", " + y);
        // }
    }

    private List<Vector3> GetPath(Vector3 startPosition, Vector3 targetPosition)
    {
        return pathFinding.GetPath(startPosition, targetPosition);
    }
    
}
