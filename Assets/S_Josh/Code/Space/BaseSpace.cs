using UnityEngine;

public abstract class BaseSpace : MonoBehaviour
{
    public IntVariable TotalMoney;
    protected UnlockSeatEvent UnlockSeatEvent;
    public bool IsUnlocked = false;
    public int Cost = 20;

    // public void OnMouseDown()
    // {
    //     if(IsUnlocked)
    //     {
    //         ShowContent();
    //     }
    //     else
    //     {
    //         if(TotalMoney.Value >= BuyEvent.Cost)
    //         {
    //             //Debug.Log("Space is locked, showing confirm menu");
    //             ConfirmBuyEvent.RaiseEvent(BuyEvent);
    //         }
    //         else
    //         {
    //             //Debug.Log("Not enough money to unlock the space");
    //         }
    //     }
    // }

    public virtual void Buy()
    {
        //sDebug.Log("Space is unlocked");
        TotalMoney.Value -= Cost;
        IsUnlocked = true;
        if(UnlockSeatEvent != null)
        {
            UnlockSeatEvent.RaiseEvent(this);
        }
        else
        {
            Debug.LogWarning("UnlockSeatEvent is null");
        }
        
    }

    // This method will be overridden by subclasses to show specific content
    protected abstract void ShowContent();

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        BaseSpace other = (BaseSpace)obj;
        if(gameObject == null || other.gameObject == null)
        {
            return false;
        }
        return gameObject == other.gameObject;
    }
    public override int GetHashCode()
    {
        return gameObject.GetHashCode();
    }
}