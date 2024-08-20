using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
     public ListGameObjectVariable SeatedTargets;
     public VoidEventChannel UpdateTarget;
     public List<Target> AllTargets = new List<Target>();

     public ListBaseSeatVariable unlockedSeats;
     public ListBaseSeatVariable UnlockedOrderSpaces;
     public VoidEventChannel StartOrderEvent;
     public IntVariable AllMoney;
     Target currentTarget;

     public VoidEventChannel OnDeathEvent;

     int currentCustomerCounted = 0;

    private void OnEnable() {
        SeatedTargets.listGameObject.Clear();
        OnDeathEvent.OnEventRaised += OnTargetDeath;
        StartOrderEvent.OnEventRaised += StartOrder;
    }

    private void OnDisable() {
        SeatedTargets.listGameObject.Clear();
        OnDeathEvent.OnEventRaised -= OnTargetDeath;
        StartOrderEvent.OnEventRaised -= StartOrder;
    }
    private void Start() {
        if(AllTargets.Count == 0)
        {
            //Debug.Log("No targets available");
            return;
        }
        currentTarget = AllTargets[0].GetComponent<Target>();
        StartOrder();
    }
    public void OnTargetDeath()
    {
        Debug.Log("Target died");
        currentCustomerCounted -= 1;
    }


    public void StartOrder()
    {
        if(!CheckSeatAvailability() || unlockedSeats.baseSpaces.Count == 0 || currentTarget == null || !CheckOrderAvailability() || UnlockedOrderSpaces.baseSpaces.Count == 0 || currentCustomerCounted >= unlockedSeats.baseSpaces.Count)
        {
            Debug.Log("No seats available");
            return;
        }
        if(AllTargets.Count == 0)
        {
            Debug.Log("Finished all orders");
            return;
        }
        
        FinishOrder();
        currentCustomerCounted++;
    }


    public void FinishOrder()
    {
        AssignOrderMachine();
    }

    public void AssignOrderMachine()
    {
        
        foreach(BaseSpace baseSpace in UnlockedOrderSpaces.baseSpaces)
        {
            if(baseSpace is OrderingSpace)
            {
                OrderingSpace orderingSpace = (OrderingSpace)baseSpace;
                if(orderingSpace.IsUnlocked)
                {
                    if(orderingSpace.GetComponentInChildren<OrderMachine>().IsAvailable)
                    {
                        currentTarget = AllTargets[0].GetComponent<Target>();
                        OrderMachine _orderMachine = orderingSpace.GetComponentInChildren<OrderMachine>();
                        _orderMachine.IsAvailable = false;
                        currentTarget.StartToGetMad = true;
                        MoveToSeat(orderingSpace, currentTarget);
                        StartCoroutine(LoadProgressBar(_orderMachine.timeToOrderMultiplier * currentTarget.TimeToOrder, _orderMachine.progressBar, _orderMachine, currentTarget));
                        AllTargets.Remove(currentTarget);
                        return;
                    }
                }
                else
                {
                    Debug.LogError("Order machine is not unlocked when it is suppose to be unlocked");
                }
            }
        }
    }




    public void AssignSeat(Target CurrentOrderTarget)
    {
        foreach(BaseSpace baseSpace in unlockedSeats.baseSpaces)
        {
            if(baseSpace is SeatSpace)
            {
                SeatSpace seatSpace = (SeatSpace)baseSpace;
                if(seatSpace.IsUnlocked)
                {
                    if(seatSpace.currentTarget == null)
                    {
                        seatSpace.currentTarget = CurrentOrderTarget;
                        CurrentOrderTarget.CurrentSeat = seatSpace;
                        SeatedTargets.listGameObject.Add(CurrentOrderTarget.gameObject);
                        MoveToSeat(seatSpace, CurrentOrderTarget);
                        CurrentOrderTarget.originalPosition = CurrentOrderTarget.transform.localPosition;
                        UpdateTarget.RaiseEvent();
                        
                        
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

    void MoveToSeat(BaseSpace seatSpace, Target target)
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
        foreach(BaseSpace baseSpace in UnlockedOrderSpaces.baseSpaces)
        {
            if(baseSpace is OrderingSpace)
            {
                OrderingSpace seatSpace = (OrderingSpace)baseSpace;
                if(seatSpace.GetComponentInChildren<OrderMachine>().IsAvailable)
                {
                    return true;
                }
            }
        }
        return false;
    }


    private IEnumerator LoadProgressBar(float timeToFill, Slider progressBar, OrderMachine orderMachine, Target CurrentOrderTarget)
    {
        //Debug.Log("Loading ordering progress bar");
        progressBar.gameObject.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < timeToFill)
        {
            // If the target is destroyed during this time, exit the coroutine early
            if (this == null)
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            progressBar.value = Mathf.Clamp01(elapsedTime / timeToFill);
            yield return null;
        }
        progressBar.gameObject.SetActive(false);
        orderMachine.IsAvailable = true;
        AllMoney.Value += CurrentOrderTarget.MoneyValue;
        AssignSeat(CurrentOrderTarget);
        StartOrder();
    }






}
