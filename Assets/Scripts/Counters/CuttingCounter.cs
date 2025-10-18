using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeArry;


    private int cuttingProgress;


    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    cuttingProgress = 0;
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxProgress
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
            }
        }
    }

    public override void InteractAltrnate(PlayerController player)
    {
        //Debug.Log(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()));
        

        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            //cut
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxProgress
            });

            if (cuttingProgress >= cuttingRecipeSO.maxProgress)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestorySelf();

                KitchenObject.SpwanKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKictchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKictchenObjectSO);
        return cuttingRecipeSO!=null;
    }
    
    private KitchenObjectSO GetOutForInput(KitchenObjectSO inputKictchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKictchenObjectSO);
        if(cuttingRecipeSO!= null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO InputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeArry)
        {
            if (cuttingRecipeSO.input == InputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }


}
