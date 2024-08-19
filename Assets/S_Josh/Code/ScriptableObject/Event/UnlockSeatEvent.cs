using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Unlock Seat Event Channel")]
public class UnlockSeatEvent : ScriptableObject
{
    public UnityAction<BaseSpace> OnEventRaised;

    public void RaiseEvent(BaseSpace baseSpace)
    {
        OnEventRaised?.Invoke(baseSpace);
    }
}
