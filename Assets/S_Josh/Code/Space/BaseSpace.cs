using UnityEngine;

public abstract class BaseSpace : MonoBehaviour
{
    public IntVariable TotalMoney;
    public BuyEvent BuyEvent;
    public ConfirmBuyEvent ConfirmBuyEvent;
    protected bool IsUnlocked = false;

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
            Debug.Log("Space is locked, showing confirm menu");
            ConfirmBuyEvent.RaiseEvent(BuyEvent);
        }
    }

    public void Buy()
    {
        if(TotalMoney.Value >= BuyEvent.Cost)
        {
            Debug.Log("Space is unlocked");
            TotalMoney.Value -= BuyEvent.Cost;
            IsUnlocked = true;
        }
        else
        {
            Debug.Log("Not enough money to unlock the space");
        }
    }

    // This method will be overridden by subclasses to show specific content
    protected abstract void ShowContent();
}