using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public List<OrderingSpace> AllOrderMachines = new List<OrderingSpace>();
    public ListBaseSeatVariable UnlockedOrderSpaces;
    public List<OrderingSpace> LockedOrderSpaces = new List<OrderingSpace>();

    public UnlockSeatEvent unlockSeatEvent;
    
    private void OnEnable() {
        LockedOrderSpaces.Clear();
        UnlockedOrderSpaces.baseSpaces.Clear();
        unlockSeatEvent.OnEventRaised += UnlockSeat;
    }
    private void OnDisable() {
        LockedOrderSpaces.Clear();
        UnlockedOrderSpaces.baseSpaces.Clear();
        unlockSeatEvent.OnEventRaised -= UnlockSeat;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(OrderingSpace seat in AllOrderMachines)
        {
            if (seat.IsUnlocked)
            {
                UnlockedOrderSpaces.baseSpaces.Add(seat);
            }
            else
            {
                LockedOrderSpaces.Add(seat);
            }
        }
    }

    

    void UnlockSeat(BaseSpace seat)
    {
        if(seat is OrderingSpace)
        {
            Debug.Log("Unlocking seat" + seat.name);
            OrderingSpace seatSpace = (OrderingSpace)seat;
            if(LockedOrderSpaces.Contains(seatSpace))
            {
                LockedOrderSpaces.Remove(seatSpace);
                UnlockedOrderSpaces.baseSpaces.Add(seat);
            }
            else
            {
                Debug.LogError("Seat does not exist in locked seats");
            }
        }
        else
        {
            Debug.Log("Seat is not a SeatSpace");
        }
       
    }



}
