using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public ListMaterialtVariable materialList;
    public IntVariable Money;
    public GameObject materialPrefab;
    List<GameObject> materialObjects;

    private void Start() {
        materialObjects = new List<GameObject>();
        foreach (RawMaterial material in materialList.materialCostEntries) {
            GameObject materialObject = Instantiate(materialPrefab, materialPrefab.transform.parent);
            materialObject.GetComponentInChildren<RawMaterialToText>().materialVariable = material;
            materialObject.GetComponentInChildren<BuyMaterial>().material = material;
            materialObject.GetComponentInChildren<BuyMaterial>().Money = Money;
            materialObject.SetActive(true);
            materialObjects.Add(materialObject);
        }
    }
}
