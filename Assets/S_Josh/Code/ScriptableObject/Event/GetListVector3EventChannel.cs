using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/GetListVector3EventChannel")]
public class GetListVector3EventChannel : ScriptableObject
{
    public Func<Vector3, Vector3, List<Vector3>> OnEventRaised;

    public List<Vector3> RaiseEvent(Vector3 StartPosition, Vector3 targetPosition)
    {
        
        return OnEventRaised?.Invoke(StartPosition, targetPosition);
    }
}
