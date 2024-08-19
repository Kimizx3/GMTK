using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public float Damage;
    public float AttackRate;
    public List<RawMaterial> RequriedMaterials;

    public void Attack()
    {
        //check if there is enough material in the inventory
        
        //if there is enough material, attack

        //if there is not enough material, show a bubble saying out of material
    }
}
