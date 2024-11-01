using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public ListBaseSeatVariable UnlockedOrderSpaces;

    public UnlockSeatEvent unlockSeatEvent;
    
    private void OnEnable() {
        UnlockedOrderSpaces.baseSpaces.Clear();
        unlockSeatEvent.OnEventRaised += UnlockSeat;
    }
    private void OnDisable() {
        UnlockedOrderSpaces.baseSpaces.Clear();
        unlockSeatEvent.OnEventRaised -= UnlockSeat;
    }

    

    void UnlockSeat(BaseSpace seat)
    {
        if(seat is OrderingSpace)
        {
            UnlockedOrderSpaces.baseSpaces.Add(seat);
        }
        else
        {
            Debug.Log("Seat is not a Ordering Space");
        }
       
    }



}
