using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Confirm Buy Event Channel")]
public class ConfirmBuyEvent : ScriptableObject
{
   public UnityAction<BuyEvent> OnEventRaised;

    public void RaiseEvent(BuyEvent buyEvent)
    {
        OnEventRaised?.Invoke(buyEvent);
    }
}
