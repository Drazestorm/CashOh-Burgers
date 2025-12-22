using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    public static event EventHandler OnAnyObjectPlacedHere;

    [SerializeField] protected Transform CounterTopPoint;
    protected KitchenObject kitchenObject;
 
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter Interact was called. This should never happen!");
    }

    public virtual void InteractCutting(Player player)
    {
        
    }

    public Transform GetKitchenObjectFollowTransform() {
        return CounterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

}
