using System;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawnd;
    public event EventHandler OnPlateRemoved;
    
    private float spwanPlateTimer;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    [SerializeField] private float spwanPlateTimerMax = 4f;

    private int platsSpawndAmmount;
    private int platsSpawndAmmountMax = 4;


    private void Update() {
        spwanPlateTimer += Time.deltaTime;

        if (spwanPlateTimer >= spwanPlateTimerMax) {
            spwanPlateTimer = 0f;

            if (platsSpawndAmmount < platsSpawndAmmountMax) {
                platsSpawndAmmount++;

                OnPlateSpawnd?.Invoke(this, EventArgs.Empty);
                
            }
        }   
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject()) {
            if (platsSpawndAmmount > 0) {
                platsSpawndAmmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
