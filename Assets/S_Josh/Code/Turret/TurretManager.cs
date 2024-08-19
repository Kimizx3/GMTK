using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    // public List<Turret> turrets = new List<Turret>();
    // public List<GameObject> getTargets = new List<GameObject>();


    // public ListBaseSeatVariable targets;

    // public VoidEventChannel OnTargetDied;

    // private void OnEnable() {
    //     OnTargetDied.OnEventRaised += RemoveTarget;
    //     foreach (GameObject target in getTargets)
    //     {
    //         AddTarget(target);
    //     }
    // }
    // private void OnDisable() {
    //     OnTargetDied.OnEventRaised -= RemoveTarget;
    // }
    // public void AddTurret(Turret turret)
    // {
    //     turrets.Add(turret);
    // }
    // public void RemoveTurret(Turret turret)
    // {
    //     turrets.Remove(turret);
    // }

    // void UpdateTarget()
    // {
    //     if (targets.baseSpaces.Count == 0)
    //     {
    //         foreach (Turret turret in turrets)
    //         {
    //             turret.CurrentTarget = null;
    //         }
    //         return;
    //     }
    //     foreach (Turret turret in turrets)
    //     {
    //         turret.CurrentTarget = targets.Peek().GetComponent<Target>();
    //     }
    // }



    // public void AddTarget(GameObject target)
    // {
    //     targets.Enqueue(target);
    //     UpdateTarget();
    // }
    // public void RemoveTarget()
    // {
    //     if (targets.Count > 0)
    //     {
    //         targets.Dequeue();
    //         UpdateTarget();
    //     }
    // }

}
