using System;
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
     public GetListVector3EventChannel GetPathEvent;

     int currentCustomerCounted = 0;
     int currentCustomerIndex = 0;

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
       
        StartOrder();
    }
    public void OnTargetDeath()
    {
        Debug.Log("Target died");
        currentCustomerCounted -= 1;
    }


    public void StartOrder()
    {   
        Debug.Log("Starting order");
        if(CheckSeatAvailability() && CheckOrderAvailability() && currentCustomerCounted <= AllTargets.Count)
        {
           if(currentCustomerCounted >= AllTargets.Count)
            {
                Debug.Log("All customers have been seated");
                return;
            }
            else
            {
                AssignOrderMachine(AllTargets[currentCustomerIndex]);
                currentCustomerIndex += 1;
                currentCustomerCounted += 1;
            }
        }
        
        
        
        
    }


    public void AssignOrderMachine(Target currentTarget)
    {
        
        foreach(BaseSpace baseSpace in UnlockedOrderSpaces.baseSpaces)
        {
            if(baseSpace is OrderingSpace)
            {
                OrderingSpace orderingSpace = (OrderingSpace)baseSpace;
                if(orderingSpace.IsUnlocked && orderingSpace.GetComponentInChildren<OrderMachine>().IsAvailable)
                {
                        OrderMachine _orderMachine = orderingSpace.GetComponentInChildren<OrderMachine>();
                        _orderMachine.IsAvailable = false;
                        currentTarget.StartToGetMad = true;
                        MoveToSeat(orderingSpace, currentTarget, () => 
                        {
                            StartCoroutine(LoadProgressBar(_orderMachine.timeToOrderMultiplier * currentTarget.TimeToOrder, _orderMachine.progressBar, _orderMachine, currentTarget));
                            
                        });
                        break;
                }
                else
                {
                    Debug.Log("Order machine is not unlocked or not available");
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
                        SeatedTargets.listGameObject.Add(CurrentOrderTarget.gameObject);
                        CurrentOrderTarget.CurrentSeat = seatSpace;
                        // CurrentOrderTarget.originalPosition = CurrentOrderTarget.transform.localPosition;
                        MoveToSeat(seatSpace, CurrentOrderTarget , () => 
                        { 
                            UpdateTarget.RaiseEvent();   
                            StartOrder();
                        });
                        
                        break;
                    }
                }
                else
                {
                    Debug.LogError("Seat is not unlocked when it is suppose to be unlocked");
                }
            }
        }
        
    }

    void MoveToSeat(BaseSpace seatSpace, Target target, Action OnComplete)
    {
        StartCoroutine(MoveToSeatCoroutine(seatSpace, target, OnComplete));
    }


    IEnumerator MoveToSeatCoroutine(BaseSpace seatSpace, Target target, Action OnComplete)
    {
        target.WalkingPath = GetPathEvent.RaiseEvent(target.transform.position, seatSpace.transform.position);
        // target.transform.position = seatSpace.transform.position;
       // target.originalPosition = seatSpace.transform.position;
        while(target.WalkingPath != null)
        {
            yield return null;
        }
        OnComplete?.Invoke();
        Debug.Log("Customer has reached the seat");
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
