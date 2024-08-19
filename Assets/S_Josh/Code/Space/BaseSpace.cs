using UnityEngine;

public abstract class BaseSpace : MonoBehaviour
{
    public IntVariable TotalMoney;
    public BuyEvent BuyEvent;
    public ConfirmBuyEvent ConfirmBuyEvent;
    public bool IsUnlocked = false;

    private void Awake() {
        BuyEvent.OnEventRaised += Buy;
    }
    
    private void OnDestroy() {
        BuyEvent.OnEventRaised -= Buy;
    }

    public void OnMouseDown()
    {
        if(IsUnlocked)
        {
            ShowContent();
        }
        else
        {
            if(TotalMoney.Value >= BuyEvent.Cost)
            {
                Debug.Log("Space is locked, showing confirm menu");
                ConfirmBuyEvent.RaiseEvent(BuyEvent);
            }
            else
            {
                Debug.Log("Not enough money to unlock the space");
            }
        }
    }

    public virtual void Buy()
    {
        
            Debug.Log("Space is unlocked");
            TotalMoney.Value -= BuyEvent.Cost;
            IsUnlocked = true;
       
    }

    // This method will be overridden by subclasses to show specific content
    protected abstract void ShowContent();

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        BaseSpace other = (BaseSpace)obj;
        return gameObject == other.gameObject;
    }
    public override int GetHashCode()
    {
        return gameObject.GetHashCode();
    }
}