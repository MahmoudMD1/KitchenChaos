using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO() 
    {
        return kitchenObjectSO;
    }


    public void SetKitchenObjectParent(IKitchenObjectParent kitchenobjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenobjectParent;

        if (kitchenobjectParent.HasKitchenObject())
        {
            Debug.LogError("Error");
        }
        kitchenobjectParent.SetKitchenObject(this);

        transform.parent = kitchenobjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
}
