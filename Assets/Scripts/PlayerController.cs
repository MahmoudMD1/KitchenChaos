using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour ,IKitchenObjectParent
{
    public static PlayerController Instance{ get; private set; }

    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    //public event EventHandler OnSelectedCounterChanged;



    [SerializeField]private float moveSpeed = 7f, rotateSpeed = 13, playerRadius=.7f , playerHight = 2 , moveDistance , intractedDistance=2f;
    [SerializeField] LayerMask counterLayerMask;

    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking ,canMove;

    private BaseCounter selectedCounter;

    public GameInput gameInput;

    private Vector3 lastInteractDir;

    private KitchenObject KitchenObject;

    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Error");
        }
        Instance = this;
    }
    
    private void Start()
    {
        gameInput.OnInteractInput += GameInput_OnInteractInput;
    }

    private void GameInput_OnInteractInput(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {

        HandelMovement();
        HandleIntraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandelMovement()
    {

        
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        moveDistance = moveSpeed * Time.deltaTime;

        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDir, moveDistance);


        if (!canMove)
        {
            // attmpt x movment

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //attemp z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }

            }

        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    private void HandleIntraction()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        //Debug.Log(lastInteractDir);

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit rayCastHit, intractedDistance, counterLayerMask))
        {
            //Debug.Log(Physics.Raycast(transform.position, lastInteractDir, out rayCastHit, intractedDistance));

            if (rayCastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //has Clear Counter
                //
                //clearCounter.Interact();
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);

            }
        }
        else
        {
            //Debug.Log(rayCastHit.transform.TryGetComponent(out clearCounter));
            // Debug.Log(clearCounter); /// is null why ?
            SetSelectedCounter(null);
        }
         
    }
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter=selectedCounter;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangeEventArgs
            {
                selectedCounter = selectedCounter

            });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.KitchenObject = kitchenObject; //this.KitchenObject = KitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return KitchenObject;
    }

    public void ClearKitchenObject()
    {
        KitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return KitchenObject != null;
    }
}
