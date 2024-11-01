using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BuildingGhost : MonoBehaviour
{
   private Transform visual;
    public GridBuildingSystemVariable gridBuildingSystemVariable;
    public VoidEventChannel RefreshVisualEvent;

    private void Awake() {
        RefreshVisualEvent.OnEventRaised += RefreshVisual;
    }
    private void OnDisable() {
        RefreshVisualEvent.OnEventRaised -= RefreshVisual;
    }
    private void Start() {
        RefreshVisual();

    }

    

    private void LateUpdate() {
        if(gridBuildingSystemVariable.gridBuildingSystem == null || !gridBuildingSystemVariable.gridBuildingSystem.isBuilding) return;
        PlacedObjectTypeSO placedObjectTypeSO = gridBuildingSystemVariable.gridBuildingSystem.GetPlacedObjectTypeSO();
        Vector3 targetPosition = gridBuildingSystemVariable.gridBuildingSystem.GetMouseWorldSnappedPosition();
        transform.position = Vector3.Lerp(transform.position, targetPosition + new Vector3(placedObjectTypeSO.Width*5, placedObjectTypeSO.Height*5), Time.deltaTime * 15f);

        transform.rotation = Quaternion.Lerp(transform.rotation, gridBuildingSystemVariable.gridBuildingSystem.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    private void RefreshVisual() {
        if(gridBuildingSystemVariable.gridBuildingSystem == null || !gridBuildingSystemVariable.gridBuildingSystem.isBuilding) 
        {
            if (visual != null) {
            Destroy(visual.gameObject);
            visual = null;
            }
            return;
        }   
       
        if (visual != null) {
            Destroy(visual.gameObject);
            visual = null;
        }

        PlacedObjectTypeSO placedObjectTypeSO = gridBuildingSystemVariable.gridBuildingSystem.GetPlacedObjectTypeSO();

        if (placedObjectTypeSO != null) {
            visual = Instantiate(placedObjectTypeSO.Visual, Vector3.zero , Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
        }
    }

}
