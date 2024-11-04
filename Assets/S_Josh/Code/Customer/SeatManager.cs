using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatManager : SpaceManager
{
    public override void UnlockSeat(BaseSpace seat)
    {
        
        if(seat is SeatSpace)
        {
            unlockedSeats.baseSpaces.Add(seat);
        }
        else
        {
            Debug.Log("Seat is not a SeatSpace");
        }
       
    
    }
}
