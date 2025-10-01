using UnityEngine;

public class CuttingCounter : BaseCounter
{

    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeArry;
    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {

                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
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
        Debug.Log(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()));
        

        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //cut
            KitchenObjectSO outputKitchenObjectSO = GetOutForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestorySelf();

            KitchenObject.SpwanKitchenObject(outputKitchenObjectSO, this);
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKictchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipe in cuttingRecipeArry)
        {
            if (cuttingRecipe.input == inputKictchenObjectSO)
            {
                return true;
            }
            
        }
        return false;

    }
    
    private KitchenObjectSO GetOutForInput(KitchenObjectSO inputKictchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipe in cuttingRecipeArry)
        {
            if (cuttingRecipe.input == inputKictchenObjectSO)
            {
                return cuttingRecipe.output;
            }
        }
        return null;
    }


}
