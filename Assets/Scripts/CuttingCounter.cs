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
                player.GetKitchenObject().SetKitchenObjectParent(this);

            }
            else
            {

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
        if (HasKitchenObject())
        {
            //cut
            KitchenObjectSO outputKitchenObjectSO = GetOutForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestorySelf();

            KitchenObject.SpwanKitchenObject(outputKitchenObjectSO, this);
        }
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
