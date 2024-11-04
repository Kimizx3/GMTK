using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : SpaceManager
{
    public override void UnlockSeat(BaseSpace seat)
    {
        if(seat is OrderingSpace)
        {
            unlockedSeats.baseSpaces.Add(seat);
        }
        else
        {
            Debug.Log("Seat is not a Ordering Space");
        }
       
    }



}
