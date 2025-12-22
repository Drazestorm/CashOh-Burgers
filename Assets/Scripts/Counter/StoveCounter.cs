using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArges> OnStateChanged;
    public class OnStateChangedEventArges : EventArgs {
        public State state;
        
    }

    // private ProgressBarUI progressBarUI;

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {

        if (HasKitchenObject())
        {
        switch (state){

            case State.Idle:
                break;

            case State.Frying:
                FryingState();
                break;

            case State.Fried:
                BurningState();
                break;

            case State.Burned:
                break;
            }
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                
                    player.GetKitchenObject().SetKitchenObject(this);
                    fryingRecipeSO = GetFryingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());

                    
                    fryingTimer = 0f;
                    ChangeState(State.Frying);
                    NormalizedProgress(fryingTimer, fryingRecipeSO.fryingTimerMax);       
                            
                }
            }
        } else {

            if (player.HasKitchenObject()) {
                
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                        ChangeState(State.Idle);
                        ResetProgress();
                    }
                }

            } else {
                GetKitchenObject().SetKitchenObject(player);
                ChangeState(State.Idle);
                ResetProgress();
            }
            
        }
    }


    // Update frying state
    private void FryingState(){
        fryingTimer += Time.deltaTime;

        NormalizedProgress(fryingTimer, fryingRecipeSO.fryingTimerMax);

        if (fryingTimer >= fryingRecipeSO.fryingTimerMax){
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

            burningTimer = 0f;
            burningRecipeSO = GetBurningRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());

            ChangeState(State.Fried);
            
        }
    }

    // Update burning state
    private void BurningState() {
        burningTimer += Time.deltaTime;

        NormalizedProgress(burningTimer, burningRecipeSO.burningTimerMax);
        
        if (burningTimer >= burningRecipeSO.burningTimerMax){
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
            ChangeState(State.Burned);

            ResetProgress();
        }
    }

    private void ChangeState(State newState) {
        state = newState;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArges {
            state = state
        });
    }

    private void NormalizedProgress(float timer, float maxTimer) {
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
            progressNormalized = timer / maxTimer
        });
    }

    private void ResetProgress() {
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
            progressNormalized = 0f
        });
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSoWithInput(inputKitchenObjectSo);
        return fryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == inputKitchenObjectSo) {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.input == inputKitchenObjectSo) {
                return burningRecipeSO;
            }
        }
        return null;
    }
    
}
