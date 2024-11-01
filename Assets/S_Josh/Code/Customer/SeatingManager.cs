using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Keep track of the seating in the restaurant, includes locked and unlocked seats, and includes the available seats
public class SeatingManager : MonoBehaviour
{
    public ListBaseSeatVariable unlockedSeats;
    public UnlockSeatEvent unlockSeatEvent;
    
    private void OnEnable() {
        unlockedSeats.baseSpaces.Clear();
        unlockSeatEvent.OnEventRaised += UnlockSeat;
    }
    private void OnDisable() {
        unlockedSeats.baseSpaces.Clear();
        unlockSeatEvent.OnEventRaised -= UnlockSeat;
    }

    

    void UnlockSeat(BaseSpace seat)
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
