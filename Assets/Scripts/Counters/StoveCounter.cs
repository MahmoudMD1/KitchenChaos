using System;
using System.Collections;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter ,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChnageEventArgs> OnStateChange;
    public class OnStateChnageEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    private State state;

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray ;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArry;

    private float fryingTimer ,burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                    fryingTimer += Time.deltaTime;
                    //FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        
                        GetKitchenObject().DestorySelf();
                        KitchenObject.SpwanKitchenObject(fryingRecipeSO.output, this);
                        burningTimer = 0f;
                        state = State.Fried;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChange?.Invoke(this, new OnStateChnageEventArgs
                        {
                            state = state
                        });
                        


                    }
                    break;
                case State.Fried:
                    
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });
                    //FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {

                        GetKitchenObject().DestorySelf();
                        KitchenObject.SpwanKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;
                        OnStateChange?.Invoke(this, new OnStateChnageEventArgs
                        {
                            state = state
                        });
                        
                    }
                    
                    break;
                case State.Burned:
                    break;
            }
        }
        if (HasKitchenObject())
        {
            
            Debug.Log(fryingTimer);
        }
    }

    public override void Interact(PlayerController player)
    {

        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //cuttingProgress = 0;
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChange?.Invoke(this, new OnStateChnageEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }

            }
            else
            {
                // player not carring anything
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChange?.Invoke(this, new OnStateChnageEventArgs
                {
                    state = state
                });
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKictchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKictchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutForInput(KitchenObjectSO inputKictchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKictchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO InputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == InputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO InputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArry)
        {
            if (burningRecipeSO.input == InputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }


}
