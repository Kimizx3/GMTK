using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class TurretSpace : BaseSpace
{
    public Turret CurrentTurret;
    // MenuHolder menuholder;
    // Purchase purchaseScript;
    protected override void ShowContent()
    {
        if (CurrentTurret != null)
        {

            Debug.Log("Showing turret upgrade menu");
            // if (menuHolder != null)
            // {
            //     menuHolder.InstantiateMenuOption(CurrentTurret.upgradeMenuPrefab);
            // }
        }
        else
        {
            // this is for placing the turret
            
            // if (purchaseScript != null)
            // {
            //     purchaseScript.PurchaseItem();
            // }
            //
            // 
            // if (menuHolder != null)
            // {
            // 
            //     menuHolder.ShowPurchaseMenu();
            // }
            
            // currrntTurret = turret;
            Debug.Log("No turret to upgrade, showing buying turret menu");
        }
    }
}
