using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public ListMaterialtVariable materialList;
    public GameObject materialPrefab;
    List<GameObject> materialObjects;

    private void Start() {
        materialObjects = new List<GameObject>();
        foreach (RawMaterial material in materialList.materialCostEntries) {
            GameObject materialObject = Instantiate(materialPrefab, materialPrefab.transform.parent);
            materialObject.GetComponentInChildren<RawMaterialToText>().materialVariable = material;
            materialObject.SetActive(true);
            materialObjects.Add(materialObject);
        }
    }
}
