using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public List<Turret> turrets = new List<Turret>();
    public ListGameObjectVariable targets;
    public VoidEventChannel UpdateTurretsTargets;

    private void OnEnable() {
        UpdateTurretsTargets.OnEventRaised += UpdateTarget;
    }
    private void OnDisable() {
        UpdateTurretsTargets.OnEventRaised -= UpdateTarget;
    }
    public void AddTurret(Turret turret) // needs work
    {
        turrets.Add(turret);
    }
    public void RemoveTurret(Turret turret)
    {
        turrets.Remove(turret);
    }

    void UpdateTarget()
    {
        if (targets.listGameObject.Count == 0)
        {
            foreach (Turret turret in turrets)
            {
                turret.CurrentTarget.Clear();
            }
            return;
        }
        
        foreach (Turret turret in turrets)
        {
            turret.CurrentTarget.Clear();
            Debug.Log("Turret Target Count: " + turret.CurrentTarget.Count);
            for(int i = 0; i < turret.TargetCount; i++)
            {
                if(i <= targets.listGameObject.Count - 1)
                {
                    turret.CurrentTarget.Add(targets.listGameObject[i].GetComponent<Target>());
                }
                else 
                {
                    break;
                }
            }
        }
    }

}
