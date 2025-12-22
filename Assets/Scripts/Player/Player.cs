using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    public static Player Instance { get; private set; }
    
    public event EventHandler OnPickSomething;

    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangeEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [Header("Player Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;

    [Header("Player Interaction Settings")]
    [SerializeField] private float interactDistance = 0.6f;
    [SerializeField] private LayerMask countersLayerMask;

    [Header("Input System Settings")]
    [SerializeField] private GameInput gameInput;

    [Header("Kitchen Object Settings")]
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private KitchenObject kitchenObject;



    private float playerRadius = .7f;
    private float playerHeight = 2f;

    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;

    private bool isWalking;

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractCuttingAction += GameInput_OnInteractCuttingAction;
    }

    private void GameInput_OnInteractCuttingAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.InteractCutting(this);
        }
    }

    private void Awake(){
        if (Instance != null)
            Debug.LogError("There is more than one Player instance");
        Instance = this;

    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);   
        }
    }

    private void Update() {

        HandleMovement();
        HandleInteraction();
        
    }

    //Handle the Player Movement with keyboard inputs W,A,S,D
    private void HandleMovement() {
        
        // get Input vector from keyboard
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        // checking the collistion is happend? 
        bool canMove = HandleCollistion(moveDir);

        if (!canMove) {
            // one in one direction if one of them is blocked
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            // checking for only X direction
            canMove = moveDir.x != 0 && HandleCollistion(moveDirX);
            if (canMove)
                moveDir = moveDirX;
            else {
                // it can move to z direction
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && HandleCollistion(moveDirZ);
                if (canMove) 
                    moveDir = moveDirZ; //update moveDir to z direction
                
            }
        }
        
        if (canMove)
            transform.position += moveDir * Time.deltaTime * moveSpeed; // move with speed and FPS independent 

        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); // handle the eye of the player rotate with the player moves.
    }

    private bool HandleCollistion(Vector3 moveDir)
    {
        float moveDistance = moveSpeed * Time.deltaTime;
        // use the physics capsule to check that collision is happend or not
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        return canMove;
    }

    private void HandleInteraction(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        // get the move direction for interaction
        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }
        
        // if there is no move direction, we use last one
        if (lastInteractDir == Vector3.zero) {
            lastInteractDir = transform.forward;
        }

        // get the raycast origin point
        Vector3 raycastOrigin = transform.position + Vector3.up; 
        // shoot the raycast from player position to last move direction and check if it hits the counter
        bool isInteract = Physics.Raycast(raycastOrigin, lastInteractDir, 
                                        out RaycastHit raycastHit, interactDistance, countersLayerMask);
        
        if (isInteract) {
            //check if that counter has BaseCounter script
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {

                if (baseCounter != selectedCounter){
                    setSelectedCounter(baseCounter);
                }
                
            } else {
                setSelectedCounter(null);
            }
        } else {
            setSelectedCounter(null);
        }
    }

    private void setSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        // create and evenet for selected counter change
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangeEventArgs{
                selectedCounter = selectedCounter
        });
    }

    public bool IsWalking(){
        return isWalking;
    }

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnPickSomething?.Invoke(this, EventArgs.Empty);
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
