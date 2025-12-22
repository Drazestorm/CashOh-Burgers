using UnityEngine.UI;
using UnityEngine;

public class PlateIconSingleUI : MonoBehaviour
{

    [SerializeField] private Image iconImage;
    
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO) {
        iconImage.sprite = kitchenObjectSO.sprite;
    }
}
