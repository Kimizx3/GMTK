using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatSpace : BaseSpace
{
    public UnlockSeatEvent unlockSeatEvent;
    public VoidEventChannel startOrderEvent;
    public Target currentTarget;
    protected override void ShowContent()
    {
        Debug.Log("Showing seat menu");
    }

    public override void Buy()
    {
       base.Buy();
       Debug.Log("Seat bought and should appear only once");
       unlockSeatEvent.RaiseEvent(this);
       startOrderEvent.RaiseEvent();
    }
}
