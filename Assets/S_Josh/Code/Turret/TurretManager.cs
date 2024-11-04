using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : SpaceManager
{
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
        unlockedSeats.baseSpaces.Add(turret);
    }
    public void RemoveTurret(Turret turret)
    {
        unlockedSeats.baseSpaces.Remove(turret);
    }

    //Once customer reaches seat, the turrect will fire
    void UpdateTarget()
    {
        if (targets.listGameObject.Count == 0)
        {
            foreach (Turret turret in unlockedSeats.baseSpaces)
            {
                turret.CurrentTarget.Clear();
            }
            return;
        }
        //for each turret set the target's to the max of this turret's target count
        foreach (Turret turret in unlockedSeats.baseSpaces)
        {
            turret.CurrentTarget.Clear();
            //Debug.Log("Turret Target Count: " + turret.CurrentTarget.Count);
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

    public override void UnlockSeat(BaseSpace seat)
    {

        
        if(seat is Turret)
        {
            unlockedSeats.baseSpaces.Add(seat);
        }
        else
        {
            Debug.Log("Seat is not a Turret");
        }
       

    }
}
