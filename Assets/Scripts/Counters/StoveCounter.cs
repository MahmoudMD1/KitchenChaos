using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;




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

                    //FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    
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


}
