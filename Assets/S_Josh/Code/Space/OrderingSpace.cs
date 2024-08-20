using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingSpace : BaseSpace
{
    public OrderMachine orderMachine;
    public UnlockSeatEvent unlockSeatEvent;
    public VoidEventChannel startOrderEvent;
    private void Start() {
        if(IsUnlocked)
        {
            orderMachine.gameObject.SetActive(true);
            orderMachine.IsAvailable = true;
        }
        else
        {
            orderMachine.gameObject.SetActive(false);
            orderMachine.IsAvailable = false;
        }
        
    }
    protected override void ShowContent()
    {
        Debug.Log("Showing ordering menu");
    }

    override public void Buy()
    {
        base.Buy();
        Debug.Log("Ordering bought and should appear only once");
        orderMachine.IsAvailable = true;
        orderMachine.gameObject.SetActive(true);
        unlockSeatEvent.RaiseEvent(this);
        startOrderEvent.RaiseEvent();
    }
}
