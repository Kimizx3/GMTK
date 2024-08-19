using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{

    public int Damage;
    public float AttackRate;
    public Slider progressBar;
    public List<MaterialCostEntry> RequriedMaterials;
    public ListMaterialtVariable Inventory;
    bool isAttacking = false;
    public Target CurrentTarget;

    private void Update() {
        if (!isAttacking && CurrentTarget != null)
        {
            TryAttack();
        }
    }

    public void TryAttack()
    {
       foreach (MaterialCostEntry material in RequriedMaterials)
        {
            if (!HasEnoughMaterials(material.materialName, material.costAmount))
            {
                progressBar.gameObject.SetActive(false);
                return;
            }
        }
        UseMaterials();
        StartCoroutine(LoadProgressBar());
    }


    bool HasEnoughMaterials(MaterialType materialName, int requiredAmount)
    {
         RawMaterial material = Inventory.materialCostEntries.Find(m => m.MaterialType == materialName);
        return material != null && material.AmountLeft >= requiredAmount;
    }

    void UseMaterials()
    {
        foreach (MaterialCostEntry material in RequriedMaterials)
        {
            RawMaterial inventoryMaterial = Inventory.materialCostEntries.Find(m => m.MaterialType == material.materialName);
            if (inventoryMaterial != null)
            {
                inventoryMaterial.AmountLeft -= material.costAmount;
            }
            else
            {
                Debug.LogWarning($"Material {material.materialName} not found in inventory!");
            }
        }
    }
     private IEnumerator LoadProgressBar()
    {
        isAttacking = true;
        progressBar.gameObject.SetActive(true);
        progressBar.value = 0;

        float elapsedTime = 0;
        while (elapsedTime < AttackRate)
        {
            if (CurrentTarget == null) // Target might have been destroyed
            {
                Debug.Log("Target destroyed before attack could be completed.");
                progressBar.gameObject.SetActive(false);
                isAttacking = false;
                yield break; // Stop the coroutine
            }

            elapsedTime += Time.deltaTime;
            progressBar.value = Mathf.Clamp01(elapsedTime / AttackRate);
            yield return null;
        }

        Attack();
        isAttacking = false;
        progressBar.gameObject.SetActive(false);
        // Attack once the progress bar is fully loaded
        
    }

    void Attack()
    {
        if (CurrentTarget == null)
        {
            Debug.LogWarning("Target is null, attack canceled.");
            return;
        }

        Debug.Log("Attacked");
        CurrentTarget.TakeDamage(Damage);
        
        // Attack logic here
    }

}
