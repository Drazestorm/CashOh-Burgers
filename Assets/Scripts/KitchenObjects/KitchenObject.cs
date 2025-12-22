using UnityEngine;

public class KitchenObject : MonoBehaviour {
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetKitchenObject(IKitchenObjectParent kitchenObject)
    {
        //clear the old counter
        if (this.kitchenObjectParent != null){
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObject;
        if (kitchenObject.HasKitchenObject()){
            Debug.LogError("KitchenObjectParent already has a kitchen object!");
        }
        kitchenObject.SetKitchenObject(this);

        // set the parent transfrom to move
        transform.parent = kitchenObject.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject){
        if (this is PlateKitchenObject){
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        } else {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObject(kitchenObjectParent);
        return kitchenObject;
    }
}
