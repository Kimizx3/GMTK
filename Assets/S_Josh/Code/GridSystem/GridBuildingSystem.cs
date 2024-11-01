using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Runtime.CompilerServices;

public class GridBuildingSystem : MonoBehaviour
{
    PlacedObjectTypeSO testTransform;
    public GridBuildingSystemVariable gridBuildingSystemVariable;
    [SerializeField]
    List<PlacedObjectTypeSO> testTransformList;
    public Grid<PathNode> grid;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Up;
    public VoidEventChannel RefreshVisualEvent;
    private Vector2Int currentMouseXY = new Vector2Int(0, 0);

    public IntVariable TotalMoney;
    public bool isBuilding = false;
    private void Awake() {
        testTransform = testTransformList[0];
        gridBuildingSystemVariable.gridBuildingSystem = this;
    }
    private void OnDisable() {
        gridBuildingSystemVariable.gridBuildingSystem = null;   
    }


    // public class Building
    // {
    //     private Grid<Building> grid;
    //     private int x;
    //     private int y;
    //     private PlacedObject transform;
    //     public Building(Grid<Building> grid, int x, int y)
    //     {
    //         this.grid = grid;
    //         this.x = x;
    //         this.y = y;
    //     }
    //     public void SetTransform(PlacedObject transform)
    //     {
    //         this.transform = transform;
    //         grid.TriggerGridObjectChanged(x, y);
    //     }
    //     public void ClearTransform()
    //     {
    //         transform = null;
    //         grid.TriggerGridObjectChanged(x, y);
    //     }
    //     public PlacedObject GetPlacedObject()
    //     {
    //         return transform;
    //     }

    //     public bool CanBuild()
    //     {
    //         return transform == null;
    //     }
    //     public override string ToString()
    //     {
    //         return x + ", " + y + "\n" + transform;
    //     }
    // }

    private void Update()
    {
        if(currentMouseXY != GetGridPosition(UtilsClass.GetMouseWorldPosition()))
        {
            currentMouseXY = GetGridPosition(UtilsClass.GetMouseWorldPosition());
            RefreshVisualEvent.RaiseEvent();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if(isBuilding)
            {
                isBuilding = false;
            }
            else
            {
                isBuilding = true;
            }
        }
        if(isBuilding)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(TotalMoney.Value < testTransform.Prefab.GetComponent<BaseSpace>().Cost)
                {
                    UtilsClass.CreateWorldTextPopup("Not Enough Money", UtilsClass.GetMouseWorldPosition());
                    return;
                }
                grid.GetXY(UtilsClass.GetMouseWorldPosition(), out int x, out int y);
                List<Vector2Int> gridPositionList = testTransform.GetGridPositionList(new Vector2Int(x, y), dir);
                PathNode building = grid.GetValue(x, y);
                bool _canBuild = true;
                foreach(Vector2Int gridPosition in gridPositionList)
                {

                    if(grid.GetValue(gridPosition.x, gridPosition.y) == null || !grid.GetValue(gridPosition.x, gridPosition.y).CanBuild())
                    {
                        _canBuild = false;
                        break;
                    }
                }
                if(_canBuild){
                    Vector2Int rotationOffset = testTransform.GetRotationOffset(dir);
                    print(rotationOffset);
                    Vector3 placedBuildingPosition = grid.GetWorldPosition(x,y) + new Vector3(rotationOffset.x, rotationOffset.y, 0) * grid.GetCellSize();
                    print(placedBuildingPosition);
                    PlacedObject placedObject = PlacedObject.Create(placedBuildingPosition + new Vector3(testTransform.Width * 5, testTransform.Height * 5), testTransform, new Vector2Int(x, y), dir);
                    foreach(Vector2Int gridPosition in gridPositionList)
                    {
                        // if(grid.GetValue(gridPosition.x, gridPosition.y) != null) 
                        grid.GetValue(gridPosition.x, gridPosition.y).SetTransform(placedObject);
                        grid.GetValue(gridPosition.x, gridPosition.y).isWalkable = false;
                    }
                }
                else
                {
                    UtilsClass.CreateWorldTextPopup("Cannot build here", UtilsClass.GetMouseWorldPosition());
                }
        }
        if (Input.GetMouseButtonDown(1))
        {
            PathNode building = grid.GetValue(UtilsClass.GetMouseWorldPosition());
            PlacedObject placedObject = building.GetPlacedObject();
            if(placedObject != null)
            {
                placedObject.DestroySelf();
                List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                 foreach(Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetValue(gridPosition.x, gridPosition.y).ClearTransform();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            dir = PlacedObjectTypeSO.GetNextDir(dir);
            RefreshVisualEvent.RaiseEvent();
            UtilsClass.CreateWorldTextPopup("Rotation: " + dir, UtilsClass.GetMouseWorldPosition());
        }


        if(Input.GetKeyDown(KeyCode.Alpha1)) testTransform = testTransformList[0]; RefreshVisualEvent.RaiseEvent();
        if(Input.GetKeyDown(KeyCode.Alpha2)) testTransform = testTransformList[1]; RefreshVisualEvent.RaiseEvent();
        if(Input.GetKeyDown(KeyCode.Alpha3)) testTransform = testTransformList[2]; RefreshVisualEvent.RaiseEvent();
        if(Input.GetKeyDown(KeyCode.Alpha4)) testTransform = testTransformList[3]; RefreshVisualEvent.RaiseEvent();
        if(Input.GetKeyDown(KeyCode.Alpha5)) testTransform = testTransformList[4]; RefreshVisualEvent.RaiseEvent();
        
        }
        
        
    }

    
    public Vector2Int GetGridPosition(Vector3 worldPosition) {
        grid.GetXY(worldPosition, out int x, out int z);
        return new Vector2Int(x, z);
    }

    public Vector3 GetMouseWorldSnappedPosition() {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        grid.GetXY(mousePosition, out int x, out int y);

        if (testTransform != null) {
             Vector2Int rotationOffset = testTransform.GetRotationOffset(dir);
                print(rotationOffset);
                Vector3 placedBuildingPosition = grid.GetWorldPosition(x,y) + new Vector3(rotationOffset.x, rotationOffset.y, 0) * grid.GetCellSize();
                print(placedBuildingPosition);
            return placedBuildingPosition;
        } else {
            print("this is not working'");
            return mousePosition;
        }
    }

    public Quaternion GetPlacedObjectRotation() {
        if (testTransform != null) {
            return Quaternion.Euler(0, 0, testTransform.GetRoatationAngle(dir));
        } else {
            return Quaternion.identity;
        }
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO() {
        return testTransform;
    }
}
