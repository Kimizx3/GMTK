using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class TurretSpace : BaseSpace
{
    public Turret CurrentTurret;
    protected override void ShowContent()
    {
        if(CurrentTurret != null)
        {
            
            Debug.Log("Showing turret upgrade menu");
            
        }
        else
        {
            // this is for placing the turret
            // currrntTurret = turret;
            Debug.Log("No turret to upgrade, showing buying turret menu");
        }
    }
}
