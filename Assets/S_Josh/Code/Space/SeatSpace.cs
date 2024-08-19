using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatSpace : BaseSpace
{
    public UnlockSeatEvent unlockSeatEvent;
    protected override void ShowContent()
    {
        Debug.Log("Showing seat menu");
    }

    public override void Buy()
    {
       base.Buy();
       unlockSeatEvent.RaiseEvent(this);
    }
}
