using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
     public ListGameObjectVariable SeatedTargets;
     public VoidEventChannel UpdateTarget;
     public List<Target> AllTargets = new List<Target>();

     public ListBaseSeatVariable unlockedSeats;
     public VoidEventChannel StartOrderEvent;
     Target currentTarget;

    private void OnEnable() {
        SeatedTargets.listGameObject.Clear();
        StartOrderEvent.OnEventRaised += StartOrder;
    }

    private void OnDisable() {
        SeatedTargets.listGameObject.Clear();
        StartOrderEvent.OnEventRaised -= StartOrder;
    }
    private void Start() {
        if(AllTargets.Count == 0)
        {
            Debug.Log("No targets available");
            return;
        }
        currentTarget = AllTargets[0].GetComponent<Target>();
        StartOrder();
    }


    public void StartOrder()
    {
        if(!CheckSeatAvailability() || unlockedSeats.baseSpaces.Count == 0 || currentTarget == null || !CheckOrderAvailability())
        {
            Debug.Log("No seats available");
            return;
        }
        if(AllTargets.Count == 0)
        {
            Debug.Log("No targets available");
            return;
        }
        currentTarget = AllTargets[0].GetComponent<Target>();
        FinishOrder();

    }


    public void FinishOrder()
    {
        // if(SeatedTargets.listGameObject.Count == 0)
        // {
        //     Debug.Log("No targets available");
        //     return;
        // }
        //order food
        //once finished assign seats
        AssignSeat();
    }




    public void AssignSeat()
    {
        if (unlockedSeats.baseSpaces.Count == 0 || !CheckSeatAvailability())
        {
            Debug.Log("No seats available");
            return;
        }
        
        foreach(BaseSpace baseSpace in unlockedSeats.baseSpaces)
        {
            if(baseSpace is SeatSpace)
            {
                SeatSpace seatSpace = (SeatSpace)baseSpace;
                if(seatSpace.IsUnlocked)
                {
                    if(seatSpace.currentTarget == null)
                    {
                        seatSpace.currentTarget = currentTarget;
                        currentTarget.CurrentSeat = seatSpace;
                        SeatedTargets.listGameObject.Add(currentTarget.gameObject);
                        MoveToSeat(seatSpace, currentTarget);
                        UpdateTarget.RaiseEvent();
                        AllTargets.Remove(currentTarget);
                        StartOrder();
                        return;
                    }
                }
                else
                {
                    Debug.LogError("Seat is not unlocked when it is suppose to be unlocked");
                }
            }
        }
        
    }

    void MoveToSeat(SeatSpace seatSpace, Target target)
    {
        target.transform.position = seatSpace.transform.position;
       // target.originalPosition = seatSpace.transform.position;
    }

    bool CheckSeatAvailability()
    {
        foreach(BaseSpace baseSpace in unlockedSeats.baseSpaces)
        {
            if(baseSpace is SeatSpace)
            {
                SeatSpace seatSpace = (SeatSpace)baseSpace;
                if(seatSpace.currentTarget == null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool CheckOrderAvailability()
    {
        return true;
    }






}
