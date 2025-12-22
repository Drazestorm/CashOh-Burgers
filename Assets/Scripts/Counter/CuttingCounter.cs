using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {

    public static event EventHandler OnAnyCut;



    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObject(this);
                cuttingProgress = 0;

                KitchenObjectSO currentcuttingRecipe = GetKitchenObject().GetKitchenObjectSO();
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(currentcuttingRecipe);

                if (cuttingRecipeSO != null) {
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
        } else {

            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                    }
                }
            } else {
                GetKitchenObject().SetKitchenObject(player);
            }
            
        }
    }

    public override void InteractCutting(Player player)
    {
        if (HasKitchenObject()) {
            KitchenObjectSO currentcuttingRecipe = GetKitchenObject().GetKitchenObjectSO();
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(currentcuttingRecipe);

            if (outputKitchenObjectSO != null){
                cuttingProgress++;

                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);

                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(currentcuttingRecipe);

                if (cuttingRecipeSO != null) {
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });

                    if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                    }
                }
            }   
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSoWithInput(inputKitchenObjectSo);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        }
        return null;
    }

    private CuttingRecipeSO GetCuttingRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSo) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

    
}
