using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListMaterialVariable", menuName = "ScriptableObjects/ListMaterialVariable")]
public class ListMaterialtVariable : ScriptableObject
{
    public List<RawMaterial> materialCostEntries = new List<RawMaterial>();
}
