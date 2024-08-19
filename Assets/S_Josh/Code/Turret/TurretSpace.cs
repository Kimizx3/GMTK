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
            Debug.Log("No turret to upgrade, showing buying turret menu");
        }
    }
}
