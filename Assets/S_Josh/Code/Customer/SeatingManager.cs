using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Keep track of the seating in the restaurant, includes locked and unlocked seats, and includes the available seats
public class SeatingManager : MonoBehaviour
{
    public List<SeatSpace> AllSeats;
    public ListBaseSeatVariable unlockedSeats;
    private List<SeatSpace> lockedSeats = new List<SeatSpace>();
    public UnlockSeatEvent unlockSeatEvent;
    
    private void OnEnable() {
        lockedSeats.Clear();
        unlockedSeats.baseSpaces.Clear();
        unlockSeatEvent.OnEventRaised += UnlockSeat;
    }
    private void OnDisable() {
        lockedSeats.Clear();
        unlockedSeats.baseSpaces.Clear();
        unlockSeatEvent.OnEventRaised -= UnlockSeat;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(SeatSpace seat in AllSeats)
        {
            if (seat.IsUnlocked)
            {
                unlockedSeats.baseSpaces.Add(seat);
            }
            else
            {
                lockedSeats.Add(seat);
            }
        }
    }

    

    void UnlockSeat(BaseSpace seat)
    {
        if(seat is SeatSpace)
        {
            Debug.Log("Unlocking seat" + seat.name);
            SeatSpace seatSpace = (SeatSpace)seat;
            if(lockedSeats.Contains(seatSpace))
            {
                lockedSeats.Remove(seatSpace);
                unlockedSeats.baseSpaces.Add(seat);
            }
            else
            {
                Debug.LogError("Seat does not exist in locked seats");
            }
        }
        else
        {
            Debug.LogError("Seat is not a SeatSpace");
        }
       
    }
}
